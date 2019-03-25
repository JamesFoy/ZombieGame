using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    [SerializeField]
    float delay;

    [SerializeField]
    GameObject explosionEffect;

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
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get nearby objects and add force and destory/kill

        Destroy(gameObject);
    }
}
