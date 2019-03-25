using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerShooting : MonoBehaviour {

    PlayerControl playerControl;

    [SerializeField]
    ParticleSystem firingParticle;

    PlayerAnimations playerAnim;

    LineRenderer shootLine;

    CharacterAudioManager Audio;

    PlayerMovement playerMove;

    Vector3 endPoint;

    [SerializeField]
    Vector3 lineOffset;

    [SerializeField]
    Weapons weapons;

    [SerializeField]
    private Transform shootT;


    [SerializeField]
    public bool isShooting = false;
    public bool emptyGun = false;
    public bool reload = false;
    public bool throwGranade = false;

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
        shootLine = this.gameObject.GetComponentInChildren<LineRenderer>();
        shootLine.enabled = false;
        playerControl = GetComponent<PlayerControl>();
        playerMove = GetComponent<PlayerMovement>();
        laserLine = GetComponentInChildren<LineRenderer>();
        Audio = GetComponent<CharacterAudioManager>();
        playerAnim = GetComponent<PlayerAnimations>();
    }

    IEnumerator LineActive()
    {
        shootLine.enabled = true;
        yield return new WaitForSeconds(0.1f);
        shootLine.enabled = false;
    }

    IEnumerator GrenadeCanThrow()
    {
        throwGranade = true;
        yield return new WaitForSeconds(10);
        throwGranade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl.state.Triggers.Right == 1 && Time.time > nextFire && shotsDone < weapons.MaxShots)
        {
            nextFire = Time.time + fireRate;
            isShooting = true;
            shotsDone++;
            RaycastHit Hit;

            firingParticle.Play();

            Vector3 fwd = shootT.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(shootT.transform.position, fwd * 40, Color.green);

            endPoint = shootT.position + fwd * 40;
            StartCoroutine(LineActive());
            shootLine.SetPosition(0, shootT.position);
            shootLine.SetPosition(1, endPoint + lineOffset);


            if (Physics.Raycast(shootT.transform.position, fwd, out Hit, 40, layer) && Hit.collider.tag == "Enemy")
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

        if (playerControl.state.Buttons.RightShoulder == ButtonState.Pressed)
        {
            GrenadeCanThrow();
        }
    }
}
