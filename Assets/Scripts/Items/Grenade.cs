using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy
//This script is used for all of the behaviour for the grenade after it is spawned. It will start a countdown for the time limit
//of the grenade. It will also control the effects that occur to enemies around the grenade when it explodes.

namespace Items
{
    public class Grenade : MonoBehaviour
    {

        [SerializeField]
        float delay;

        [SerializeField]
        int explosionDamage = 10;

        [SerializeField]
        GameObject explosionEffect;

        [SerializeField]
        float explosionForce = 700;

        [SerializeField]
        float radius = 5f;

        public AudioClip explosionSound;

        float countDown;

        bool hasExploded = false;

        // Use this for initialization
        void Start()
        {
            countDown = delay;
        }

        // Update is called once per frame. Starts the countdown for the grenade and if it reaches 0 and hasnt exploded
        //it will call the explode funciton
        void Update()
        {
            countDown -= Time.deltaTime;

            if (countDown <= 0 && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }

        //This is the method that controls all behaviour for when the grenade explodes. It will spawn a explosion effect,
        //it will also play an audio clip when it explodes and damage any enemy around the explosion
        private void Explode()
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            //Get nearby objects and add force and destory/kill
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    //rb.AddExplosionForce(explosionForce, transform.position, radius);
                }

                if (nearbyObject.CompareTag("Enemy"))
                {
                    nearbyObject.GetComponent<AIScript>().enemy.Health -= explosionDamage;
                }
            }

            Destroy(gameObject);
        }
    }
}

