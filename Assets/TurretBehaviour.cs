using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour {

    [SerializeField]
    ParticleSystem firingParticle;

    private float nextFire = 0.0f;

    public AudioSource turretShot;

    private Transform startRotation;

    float strength = 1f;
    private Quaternion targetRotation;
    private float str;
    private Transform target;
    private bool isAiming = false;
    private float fireRate = 1;

    void Awake()
    {
        startRotation = this.gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject.transform;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            this.gameObject.transform.rotation = startRotation.rotation;
        }

        if (isAiming == true && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            turretShot.Play();
            firingParticle.Play();
            firingParticle.enableEmission = true;
            //Shoot();
        } 
    }

    private void Shoot()
    {
        //Debug.Log("Shooting");

    }

    private void LateUpdate()
    {
        Aim();
    }

    void Aim ()
    {
        if (target != null)
        {
            isAiming = true;
            //This line finds the rotation from this object to the target
            targetRotation = Quaternion.LookRotation(target.position - transform.position);
            //This line finds out how much you are wanting to lerp the rotation and prevents it from overshooting the rotation
            str = Mathf.Min(strength * Time.deltaTime, 1);
            //This line simply lerps the rotation of the object
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
        else
        {
            isAiming = false;
        }
	}
}
