using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private float CameraMoveSpeed = 120.0f;
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
    private float finalInputZ;
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
    private bool isAiming;

    [SerializeField]
    LayerMask layerMask;

    GamePadState state;

    public bool IsAiming
    {
        get
        {
            return isAiming;
        }
    }

    private void Awake()
    {
        Transform target = CameraStart.transform;
        transform.position = target.position;
        Camera.main.fieldOfView = 50;
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
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        PlayerIndex player = PlayerIndex.One;
        state = GamePad.GetState(player);

        //Keyboard & Mouse
        if (Input.GetMouseButton(1) || state.Triggers.Left == 1)
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
        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f||Mathf.Abs(Input.GetAxis("Vertical")) >= 0.1f || Mathf.Abs(state.ThumbSticks.Left.Y) >= 0.1f || Mathf.Abs(state.ThumbSticks.Left.X) >= 0.1f)
        {
            CameraUpdater();
        }
    }

    public void CameraUpdater()
    {   
        t += 1 * Time.deltaTime;
        t=Mathf.Clamp(t, 0, 2);
        Camera.main.fieldOfView = Mathf.Lerp(50, 80,t );

        //set the target object to follow
        Transform target = CameraFollowObj.transform;

        //move towards the game object that is the target
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void Aiming()
    {
        isAiming = true;
    }

    void NotAiming()
    {
        isAiming = false;
    }
}
