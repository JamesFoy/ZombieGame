using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    AimingIKControl iKControl;

    [SerializeField]
    PlayerControl playerControl;

    [SerializeField]
    Transform startPosition;

    [SerializeField]
    GameObject aimCamera;

    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    private float CameraMoveSpeed;
    public GameObject CameraFollowObj;
    float t = 0;
    Vector3 FollowPOS;
    [SerializeField]
    private float clampAngle = 80.0f;
    [SerializeField]
    private float inputSensitivity = 150.0f;
    [SerializeField]
    private GameObject CameraObj;
    [SerializeField]
    private GameObject CameraStart;
    [SerializeField]
    private GameObject PlayerObj;
    [SerializeField]
    private float camDistanceXToPlayer;
    [SerializeField]
    private float camDistanceYToPlayer;
    [SerializeField]
    private float camDistanceZToPlayer;
    [SerializeField]
    private float mouseX;
    [SerializeField]
    private float mouseY;
    [SerializeField]
    private float finalInputX;
    [SerializeField]
    private float finalInputY;
    [SerializeField]
    private float smoothX;
    [SerializeField]
    private float smoothY;
    [SerializeField]
    private float rotY = 0.0f;
    [SerializeField]
    private float rotX = 0.0f;
    [SerializeField]
    private float fovSpeed = 100f;
    [SerializeField]
    public bool isAiming;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Canvas uiCanvas;

    GamePadState state;

    public bool IsAiming
    {
        get
        {
            return isAiming;
        }
    }

    private void OnEnable()
    {
        uiCanvas.enabled = false;
        mainCamera.transform.position = startPosition.position;
        mainCamera.transform.rotation = startPosition.rotation;
    }

    private void Awake()
    {
        mainCamera.SetActive(true);
        aimCamera.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isAiming = false;
    }
	
	// Update is called once per frame
	void Update () {
        //the rotation of the sticks/mouse
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputY = Input.GetAxis("RightStickVertical");
        //mouseX = Input.GetAxis("Mouse X");
        //mouseY = Input.GetAxis("Mouse Y");
        //finalInputX = inputX + mouseX;
        //finalInputY = inputY + mouseY;

        rotY += inputX * inputSensitivity * Time.deltaTime;
        rotX += inputY * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        //Keyboard & Mouse
        if (playerControl.state.Triggers.Left == 1)
        {

            Aiming();
        }
        else
        {

            NotAiming();
        }

        RaycastHit hit;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        Debug.DrawRay(Camera.main.transform.position, transform.forward * 4, Color.red);
        if (Physics.Raycast(ray, out hit, 4, ~layerMask))
        {
            print(hit.transform.name);
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

    void Aiming()
    {
        uiCanvas.enabled = true;
        aimCamera.SetActive(true);
        mainCamera.SetActive(false);
        iKControl.activeIK = true;
        isAiming = true;
    }

    void NotAiming()
    {
        uiCanvas.enabled = false;
        aimCamera.SetActive(false);
        mainCamera.SetActive(true);
        iKControl.activeIK = false;
        isAiming = false;
    }
}
