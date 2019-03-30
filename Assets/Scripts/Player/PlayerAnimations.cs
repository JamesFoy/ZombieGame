using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    CharacterAudioManager Audio;

    PlayerShooting playerShot;

    PlayerMovement playerMove;

    HandPlacementIK handIk;

    [SerializeField]
    private CameraFollow cam;

    [SerializeField]
    private Animator anim;

    private bool Pistol = false;
    private bool TwoHanded = false;

    [SerializeField]
    private GameObject mag;
    [SerializeField]
    private GameObject mag2;

    [SerializeField]
    GameObject granade;

    [SerializeField]
    float throwForce;

    [SerializeField]
    Transform handPoint;

    [SerializeField]
    private Transform magSpawn;
    public bool isRunning;

    // Use this for initialization
    public void Start ()
    {
        playerMove = GetComponent<PlayerMovement>();
        playerShot = GetComponent<PlayerShooting>();
        Audio = GetComponent<CharacterAudioManager>();
        handIk = GetComponent<HandPlacementIK>();

        if (anim != null)
        {
            anim = GetComponent<Animator>();
        } return;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Setting all animations to the parameters
        anim.SetFloat("Horizontal", playerMove.h);
        anim.SetFloat("Vertical", playerMove.v);
        anim.SetBool("isShooting", playerShot.isShooting);
        anim.SetBool("isAiming", cam.isAiming);
        anim.SetBool("Reload", playerShot.reload);
        anim.SetBool("Pistol", Pistol);
        anim.SetBool("TwoHanded", TwoHanded);
        anim.SetBool("Running", isRunning);
        anim.SetBool("IsMoving", playerMove.moving);
        anim.SetBool("ThrowGranade", playerShot.throwGranade);
    }

    //Method use for reloading
    public void ReloadingGun()
    {
        if (playerShot.reload == true)
        {
            Audio.PlayReloadSound();
            anim.SetTrigger("Reloading");
            playerShot.emptyGun = false;
            playerShot.shotsDone = 0;
        }
        else
        {
            anim.ResetTrigger("Reloading");
        }
    }

    //These methods are used for animation events
    public void MagOff()
    {
        Instantiate(mag2, magSpawn);
        mag.SetActive(false);
    }

    public void MagOn()
    {
        mag.SetActive(true);
    }

    public void IkOff()
    {
        handIk.ikActive = false;
    }

    public void IkOn()
    {
        handIk.ikActive = true;
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(granade, handPoint.position, handPoint.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
