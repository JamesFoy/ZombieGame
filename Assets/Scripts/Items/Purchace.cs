using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using XInputDotNetPure;

//Author - James Foy
//This script is used to control all of the behaviour for purchasing defences. This script is placed on any defence and will
//allow the player to purchase the defence when near

namespace Items
{
    public class Purchace : MonoBehaviour
    {

        public int price;

        private bool beenPurchased = false;

        UIScript uiScript;

        PlayerControl playerControl;

        TurretBehaviour turretBehaviour;

        // Use this for initialization. Just setting up referneces to components
        void Start()
        {
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            uiScript = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
            turretBehaviour = this.gameObject.GetComponentInParent<TurretBehaviour>();
            turretBehaviour.enabled = false;
        }

        //Checking if the player is near and it will display the popup text on the UI
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Makes the popup display on screen
                if (beenPurchased == false)
                {
                    uiScript.DisplayPopup();
                }

                //This allows the player to purchase the defence
                if (uiScript.score >= price && playerControl.state.Buttons.A == ButtonState.Pressed && beenPurchased == false)
                {
                    Purchase();
                }
            }
        }

        //Removes the UI popup
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                uiScript.HideDisplayPopup();
            }
        }

        //This method controls all of the behaviour when purachasing the defence.
        private void Purchase()
        {
            uiScript.purchaseSound.Play();
            turretBehaviour.enabled = true;
            uiScript.score -= price;
            beenPurchased = true;
        }
    }
}

