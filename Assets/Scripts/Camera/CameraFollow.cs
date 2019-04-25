using Player;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

//Author - James Foy
//This script was use to help the camera follow the players character. It also helps with controlling aiming

namespace CameraControl
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField]
        AimingIKControl iKControl;

        [SerializeField]
        PlayerControl playerControl;

        [SerializeField]
        Transform startPosition;

        [SerializeField]
        GameObject mainCamera;

        [SerializeField]
        private float CameraMoveSpeed;
        public GameObject CameraFollowObj;
        [SerializeField]
        private float clampAngle = 80.0f;
        [SerializeField]
        private float inputSensitivity = 150.0f;
        [SerializeField]
        private float rotY;
        [SerializeField]
        private float rotX;
        [SerializeField]
        public bool isAiming;

        [SerializeField]
        LayerMask layerMask;

        [SerializeField]
        Image crossHair;

        GamePadState state;


        //Used to help other scripts know if the player is aiming
        public bool IsAiming
        {
            get
            {
                return isAiming;
            }
        }

        //this is used to help correctly position the camera when the game starts
        private void OnEnable()
        {
            crossHair.enabled = false;
            mainCamera.transform.position = startPosition.position;
            mainCamera.transform.rotation = startPosition.rotation;
        }

        // Use this for initialization
        void Start()
        {
            Vector3 rot = transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isAiming = false;
        }

        // Update is called once per frame
        void Update()
        {
            //the rotation of the sticks
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputY = Input.GetAxis("RightStickVertical");

            rotY += inputX * inputSensitivity * Time.deltaTime;
            rotX += inputY * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

            //Controller Aiming
            if (playerControl.state.Triggers.Left == 1)
            {

                Aiming();
            }
            else
            {

                NotAiming();
            }
        }


        void LateUpdate()
        {
            CameraUpdater();
        }

        public void CameraUpdater()
        {
            //set the target object to follow
            Transform target = CameraFollowObj.transform;

            //move towards the game object that is the target
            float step = CameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        //These methods are used to run different behaviour when aiming or not aiming
        void Aiming()
        {
            crossHair.enabled = true;
            Camera.main.fieldOfView = 20;
            iKControl.activeIK = true;
            isAiming = true;
        }

        void NotAiming()
        {
            crossHair.enabled = false;
            Camera.main.fieldOfView = 40;
            iKControl.activeIK = false;
            isAiming = false;
        }
    }
}

