using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour
{
    PlayerAnimations playerAnim;

    PlayerControl playerControl;

    CharacterAudioManager Audio;

    [SerializeField]
    private CameraFollow cam;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float aimSpeed;

    [SerializeField]
    private float offset = 1f;

    private float turnSmoothing = 15f; // A smoothing value for turning the player.
    private float speedDampTime = 0.1f; // The damping for the speed parameter

    [SerializeField]
    public float h; // Moving around (H & V are input)
    [SerializeField]
    public float v;

    private Rigidbody rb;

    public bool aiming = false;

    public bool moving;

    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody>();
        Audio = GetComponent<CharacterAudioManager>();
        playerControl = GetComponent<PlayerControl>();

        moving = false;
    }

    private void FixedUpdate()
    {
        h = playerControl.state.ThumbSticks.Left.X;
        v = playerControl.state.ThumbSticks.Left.Y;

        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            moving = true;
            Rotating(h, v);

            if (aiming == false)
            {
                rb.AddForce(transform.forward * speed);
            }
        }
        else
        {
            moving = false;
            Vector3 stop = new Vector3(0, 0, 0);
            rb.angularVelocity = stop;
            rb.velocity = stop;
        }

        if (playerControl.state.Buttons.LeftStick == ButtonState.Pressed && cam.isAiming == false)
        {
            speed = 20;
            playerAnim.isRunning = true;
        }
        else
        {
            speed = 11;
            playerAnim.isRunning = false;
        }
    }

    public void Rotating(float horizontal, float vertical)
    {

        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

        targetDirection = Camera.main.transform.TransformDirection(targetDirection);

        targetDirection.y = 0.0f;

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        if (cam.IsAiming == true)
        {
            WhileAiming();
        }
        else
        {
            aiming = false;
            // Change the players rotation to this new rotation.
            transform.rotation = newRotation;
        }
    }

    public void WhileAiming()
    {
        aiming = true;
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        if (h != 0)
        { 
            
            rb.AddForce(Camera.main.transform.right * h * speed);
        }

        if (v != 0)
        {
            
            rb.AddForce(Camera.main.transform.forward  * v * speed);
        }
    }
}
