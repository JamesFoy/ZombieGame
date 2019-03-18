using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerShooting : MonoBehaviour {

    PlayerControl playerControl;

    [SerializeField]
    ParticleSystem firingParticle;

    PlayerAnimations playerAnim;

    CharacterAudioManager Audio;

    PlayerMovement playerMove;

    [SerializeField]
    Weapons weapons;

    [SerializeField]
    private Transform shootT;

    [SerializeField]
    public bool isShooting = false;
    public bool emptyGun = false;
    public bool reload = false;

    [SerializeField]
    private float fireRate;
    private float nextFire;

    [SerializeField]
    public float shotsDone = 0;

    private LineRenderer laserLine;

    [SerializeField]
    LayerMask layer;

    // Use this for initialization
    void Start ()
    {
        playerControl = GetComponent<PlayerControl>();
        playerMove = GetComponent<PlayerMovement>();
        laserLine = GetComponentInChildren<LineRenderer>();
        Audio = GetComponent<CharacterAudioManager>();
        playerAnim = GetComponent<PlayerAnimations>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerControl.state.Triggers.Right == 1 && Time.time > nextFire && shotsDone < weapons.MaxShots)
        {
            nextFire = Time.time + fireRate;
            isShooting = true;
            shotsDone++;
            RaycastHit Hit;

            firingParticle.Play();

            Vector3 fwd = shootT.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(shootT.transform.position, fwd * 100, Color.green);
            if (Physics.Raycast(shootT.transform.position, fwd, out Hit, 100, layer) && Hit.collider.tag == "Enemy")
            {
                Hit.collider.GetComponent<AIScript>().enemy.Health -= weapons.AkDamge;
            }
        }
        else
        {
            isShooting = false;
        }

        if (playerControl.state.Triggers.Right == 1 && Time.time > nextFire && shotsDone >= weapons.MaxShots)
        {
            nextFire = Time.time + fireRate;
            Audio.PlayEffect("enmpty_gun");
            emptyGun = true;
        }

        if (playerControl.state.Buttons.X == ButtonState.Pressed && shotsDone != 0)
        {
            playerAnim.ReloadingGun();
            reload = true;
        }
        else
        {
            reload = false;
        }
    }
}
