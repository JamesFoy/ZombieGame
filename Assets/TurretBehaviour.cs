using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour {

    [SerializeField]
    ParticleSystem firingParticle;

    private float nextFire;

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
            firingParticle.Stop();
            firingParticle.enableEmission = false;
            this.gameObject.transform.rotation = startRotation.rotation;
            isAiming = false;
        }

        if (isAiming == true && Time.time > nextFire)
        {
            Shoot();
        } 
    }

    private void Shoot()
    {
        nextFire = Time.time + fireRate;
        turretShot.Play();
        firingParticle.Play();
        firingParticle.enableEmission = true;

        if (target != null)
        {
            target.GetComponent<AIScript>().enemy.Health--;
        }
    }

    private void LateUpdate()
    {
        Aim();
    }

    void Aim ()
    {
        if (target)
        {
            isAiming = true;
            //This line finds the rotation from this object to the target
            targetRotation = Quaternion.LookRotation(target.position - transform.position);
            //This line finds out how much you are wanting to lerp the rotation and prevents it from overshooting the rotation
            str = Mathf.Min(strength * Time.deltaTime, 1);
            //This line simply lerps the rotation of the object
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
        else if (target == null)
        {
            target = FindClosestTarget();
            isAiming = false;
        }
    }

    private Transform FindClosestTarget()
    {
        // Find all game objects with tag Enemy

        GameObject[] enemies;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;

        var distance = Mathf.Infinity;

        var position = transform.position;

        // Iterate through them and find the closest one

        foreach (GameObject enemy in enemies)
        {
            var diff = (enemy.transform.position - position);

            var curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {

                closest = enemy;

                distance = curDistance;

            }

        }
        if (enemies != null)
        {
            return closest.transform;
        }
        return null;
    }
}
