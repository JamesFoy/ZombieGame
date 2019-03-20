using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour {

    [SerializeField]
    ParticleSystem firingParticle;

    [SerializeField]
    SpawnScript spawnScript;

    Rigidbody body;

    private float nextFire;

    Quaternion storedRot;

    public AudioSource turretShot;

    float strength = 1f;
    private Quaternion targetRotation;
    private float str;
    private Transform target;
    private bool isAiming = false;
    private float fireRate = 1;

    // Use this for initialization
    void Awake()
    {
        body = GetComponent<Rigidbody>();

        if (body == null)
        {
            Debug.LogError("Missing Rigidbody");
            return;
        }

        storedRot = body.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = FindClosestTarget();
        }
    }

    private void Update()
    {
        if (target == null)
        {
            body.rotation = Quaternion.Lerp(transform.rotation, storedRot, Time.deltaTime);
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
        target.GetComponent<AIScript>().enemy.Health--;

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
            //target = FindClosestTarget();
            isAiming = false;
        }
    }

    private Transform FindClosestTarget()
    {
        isAiming = true;

        // Find all game objects with tag Enemy

        var enemies = spawnScript.SpawnedEnemies;

        AIScript closest = null;

        var distance = Mathf.Infinity;

        var position = transform.position;

        // Iterate through them and find the closest one

        foreach (AIScript enemy in enemies)
        {
            var diff = (enemy.transform.position - position);

            var curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {

                closest = enemy;

                distance = curDistance;

            }

        }
        if (enemies.Count > 0)
        {
            return closest.transform;
        }
        return null;
    }
}
