using Enemy;
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

//Author - James Foy
//This script is used to control all of the behaviour when the player shoots

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {

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
        private float nextThrow;
        [SerializeField]
        private float throwRate;
        public int grenadesThrown;

        // Use this for initialization. Setting up references to components
        void Start()
        {
            shootLine = this.gameObject.GetComponentInChildren<LineRenderer>();
            shootLine.enabled = false;
            playerControl = GetComponent<PlayerControl>();
            playerMove = GetComponent<PlayerMovement>();
            laserLine = GetComponentInChildren<LineRenderer>();
            Audio = GetComponent<CharacterAudioManager>();
            playerAnim = GetComponent<PlayerAnimations>();
        }

        //This displays the in game line
        IEnumerator LineActive()
        {
            shootLine.enabled = true;
            yield return new WaitForSeconds(0.1f);
            shootLine.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            //This makes the player shot when pulling the right trigger. Also controls the behaviour when shooting.
            if (playerControl.state.Triggers.Right == 1 && Time.time > nextFire && shotsDone < weapons.MaxShots)
            {
                //All behaviour when the player shoots. Makes the sure there is a delay between each shot.
                //spawns the raycast to allow the player to shoot something
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

                //Checks what the raycast hits and if it was an enemy damage it.
                if (Physics.Raycast(shootT.transform.position, fwd, out Hit, 40, layer) && Hit.collider.tag == "Enemy")
                {
                    Hit.collider.GetComponent<AIScript>().enemy.Health -= weapons.AkDamge;
                }
            }
            else
            {
                isShooting = false;
            }

            //If the weapon has no ammo play sound effect
            if (playerControl.state.Triggers.Right == 1 && Time.time > nextFire && shotsDone >= weapons.MaxShots)
            {
                nextFire = Time.time + fireRate;
                Audio.PlayEffect("enmpty_gun");
                emptyGun = true;
            }

            //Allows the player to reload
            if (playerControl.state.Buttons.X == ButtonState.Pressed && shotsDone != 0)
            {
                playerAnim.ReloadingGun();
                reload = true;
            }
            else
            {
                reload = false;
            }

            //Mehtod used to throw a grenade
            if (playerControl.state.Buttons.RightShoulder == ButtonState.Pressed && Time.time > nextThrow && grenadesThrown < weapons.MaxGrenades)
            {
                nextThrow = Time.time + throwRate;
                throwGranade = true;
                grenadesThrown++;
            }
            else
            {
                throwGranade = false;
            }
        }
    }
}

