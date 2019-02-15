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

    ParticleSystem fire;

    [SerializeField]
    Weapons weapons;

    [SerializeField]
    private Transform shootT;

    [SerializeField]
    public bool isShooting = false;

    private bool aiming = false;
    public bool emptyGun = false;
    public bool reload = false;

    [SerializeField]
    private float fireRate;
    private float nextFire = 0.0f;

    [SerializeField]
    public float shotsDone = 0;

    private LineRenderer laserLine;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

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

        layer |= Physics.IgnoreRaycastLayer;
        layer = ~layer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerControl.state.Triggers.Right == 1 && Time.time > nextFire && shotsDone < weapons.MaxShots)
        {
            //StartCoroutine(ShootLaser());

            nextFire = Time.time + fireRate;
            isShooting = true;
            shotsDone++;
            RaycastHit Hit;

            firingParticle.Play();

            //laserLine.SetPosition(0, shootT.position);

            Vector3 fwd = shootT.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(shootT.transform.position, fwd * 100, Color.green);
            if (Physics.Raycast(shootT.transform.position, fwd, out Hit, 100, layer))
            {
                //laserLine.SetPosition(1, Hit.point);
                Debug.Log(Hit);

                if (Hit.collider.tag == "Enemy")
                {
                    print("enemy");
                    Hit.collider.GetComponent<Enemy>().enemy.Health -= weapons.AkDamge;
                }
            }
            else
            {
                //laserLine.SetPosition(1, shootT.transform.position + (fwd * 30));
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

    //private IEnumerator ShootLaser()
    //{
    //    laserLine.enabled = true;
    //    yield return shotDuration;
    //    laserLine.enabled = false;
    //}

    public void Shot()
    {
        //fire.Play();
    }
}
