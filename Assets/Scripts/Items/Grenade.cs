using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

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

    float countDown;

    bool hasExploded = false;

	// Use this for initialization
	void Start ()
    {
        countDown = delay;
	}

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
	}

    private void Explode()
    {
        //Show Explosion Effect;
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get nearby objects and add force and destory/kill
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }

            if (nearbyObject.CompareTag("Enemy"))
            {
                nearbyObject.GetComponent<AIScript>().enemy.Health -= explosionDamage;
            }
        }

        Destroy(gameObject);
    }
}
