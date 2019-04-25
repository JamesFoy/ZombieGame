using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy
//This script is used to control all of the behaviour for the turrets. This script will be used to fine specific 
//targets for the turrets and will also allow the target to shot and damage the target

public class TurretBehaviour : MonoBehaviour {

    [SerializeField]
    ParticleSystem firingParticle;

    Transform firePoint;

    WaveSpawner spawnScript;

    Rigidbody body;

    private float nextFire;

    Quaternion storedRot;

    LineRenderer line;

    [SerializeField]
    Vector3 bodyOffset;

    public AudioSource turretShot;

    float strength = 1f;
    private Quaternion targetRotation;
    private float str;
    private Transform target;
    private bool isAiming = false;
    private float fireRate = 1;

    //Used to display in game line of bullet from the turret to the enemy
    IEnumerator LineActive()
    {
        line.enabled = true;
        yield return new WaitForSeconds(0.1f);
        line.enabled = false;
    }

    // Use this for initialization. Sets up references to components
    void Awake()
    {
        line = this.gameObject.GetComponentInChildren<LineRenderer>();

        firePoint = this.gameObject.GetComponentInChildren<LineRenderer>().transform;

        spawnScript = GameObject.FindObjectOfType<WaveSpawner>();

        body = GetComponent<Rigidbody>();

        if (body == null)
        {
            Debug.LogError("Missing Rigidbody");
            return;
        }

        storedRot = body.rotation;
    }

    //Finds the closest target with a collider
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = FindClosestTarget();
        }
    }

    //Makes the turret reset itself if not aiming
    private void Update()
    {
        if (target == null)
        {
            body.rotation = Quaternion.Lerp(transform.rotation, storedRot, Time.deltaTime);
            isAiming = false;
            line.enabled = false;
        }

        //Makes the turret shoot if it is aiming
        if (isAiming == true && Time.time > nextFire)
        {
            Shoot();
        }
    }

    //Mehtod used for the behaviour when the turret shoots. This will play a sound, particle effect will spawn, and the in game 
    //line will spawn
    private void Shoot()
    {
        nextFire = Time.time + fireRate;
        turretShot.Play();
        firingParticle.Play();
        target.GetComponent<AIScript>().enemy.Health--;
        StartCoroutine(LineActive());
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, target.transform.localPosition + bodyOffset);
    }

    //Makes the turret aim
    private void LateUpdate()
    {
        Aim();
    }

    //Mehtod used to control what the turret does while aiming
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

    //Methods used for finding the closest target
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

        //returns the closest enemy
        if (enemies.Count > 0)
        {
            return closest.transform;
        }
        return null;
    }
}
