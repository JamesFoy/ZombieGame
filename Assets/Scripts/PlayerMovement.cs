using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerStats player;

    [SerializeField]
    Weapons weapon;

    CharacterAudioManager Audio;

    PlayerIndex one; // sets how many players are in the game using controllers. This sets 1 player as player one.  

    [SerializeField]
    private CameraFollow cam;

    [SerializeField]
    private Transform shootT;

    [SerializeField]
    private bool isShooting;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float aimSpeed;

    [SerializeField]
    private float offset = 1f;

    private float turnSmoothing = 15f; // A smoothing value for turning the player.
    private float speedDampTime = 0.1f; // The damping for the speed parameter

    [SerializeField]
    private float h; // Moving around (H & V are input)
    [SerializeField]
    private float v;

    public bool moving;

    private bool aiming;

    private bool Pistol;
    private bool TwoHanded;

    private Animator anim;

    private bool emptyGun;
    private bool Reload;

    private Rigidbody rb;

    [SerializeField]
    private GameObject mag;
    [SerializeField]
    private GameObject mag2;

    [SerializeField]
    private Transform magSpawn;

    GamePadState state;

    [SerializeField]
    private float fireRate;
    private float nextFire = 0.0f;

    [SerializeField]
    private float shotsDone = 0;

    private LineRenderer laserLine;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Audio = GetComponent<CharacterAudioManager>();
        aiming = false;
        isShooting = false;
        moving = false;
        emptyGun = false;
        Reload = false;
        Pistol = false;
        TwoHanded = false;
        laserLine = GetComponentInChildren<LineRenderer>();
    }

    private void FixedUpdate()
    {
        PlayerIndex player = PlayerIndex.One;

        state = GamePad.GetState(player);

        if (isShooting == true)
        {
            GamePad.SetVibration(player, 1, state.Triggers.Right);
            Audio.PlayGunSound();
        }
        else
        {
            GamePad.SetVibration(player, 0, 0);
        }

        h = state.ThumbSticks.Left.X;
        v = state.ThumbSticks.Left.Y;

        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);
        anim.SetBool("isShooting", isShooting);
        anim.SetBool("isAiming", aiming);
        anim.SetBool("Reload", Reload);
        anim.SetBool("Pistol", Pistol);
        anim.SetBool("TwoHanded", TwoHanded);

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
        }
    }

    private void Update()
    {
        if (state.Triggers.Right == 1 && Time.time > nextFire && shotsDone < weapon.MaxShots)
        {
            StartCoroutine(ShootLaser());

            nextFire = Time.time + fireRate;
            isShooting = true;
            shotsDone++;
            RaycastHit Hit;

            laserLine.SetPosition(0, shootT.position);

            Vector3 fwd = shootT.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(shootT.transform.position, fwd * 30, Color.green);
            if (Physics.Raycast(shootT.transform.position, fwd, out Hit, 50))
            {
                laserLine.SetPosition(1, Hit.point);

                if (Hit.collider.tag == "Enemy")
                {
                    print("enemy");
                    Hit.collider.GetComponent<Enemy>().enemy.Health -= weapon.AkDamge;
                }
            }
            else
            {
                laserLine.SetPosition(1, shootT.transform.position + (fwd * 30));
            }
        }
        else
        {
            isShooting = false;
        }

        

        if (state.Triggers.Right == 1 && Time.time > nextFire && shotsDone >= weapon.MaxShots)
        {
            nextFire = Time.time + fireRate;
            Audio.PlayEffect("enmpty_gun");
            emptyGun = true;
        }

        if (state.Buttons.X == ButtonState.Pressed)
        {
            ReloadingGun();
            Reload = true;
        }
        else
        {
            Reload = false;
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
            
            rb.AddForce(transform.right * h * aimSpeed);
        }

        if (v != 0)
        {
            
            rb.AddForce(transform.forward  * v * aimSpeed);
        }
    }

    public void ReloadingGun()
    {
        if (Reload == true)
        {
            Audio.PlayReloadSound();
            anim.SetTrigger("Reloading");
            emptyGun = false;
            shotsDone = 0;
        }
    }

    public void MagOff()
    {
        Instantiate(mag2, magSpawn);
        mag.SetActive(false);
    }

    public void MagOn()
    {
        mag.SetActive(true);
    }

    private IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
