using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Purchace : MonoBehaviour {

    public int price;

    private bool beenPurchased = false;

    UIScript uiScript;

    PlayerControl playerControl;

    TurretBehaviour turretBehaviour;

	// Use this for initialization
	void Start ()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        uiScript = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
        turretBehaviour = this.gameObject.GetComponentInParent<TurretBehaviour>();
        turretBehaviour.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (beenPurchased == false)
            {
                uiScript.DisplayPopup();
            }

            if (uiScript.score >= price && playerControl.state.Buttons.A == ButtonState.Pressed && beenPurchased == false)
            {
                Purchase();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            uiScript.HideDisplayPopup();
        }
    }

    private void Purchase()
    {
        uiScript.purchaseSound.Play();
        turretBehaviour.enabled = true;
        uiScript.score -= price;
        beenPurchased = true;
    }
}
