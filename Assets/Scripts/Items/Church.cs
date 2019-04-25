using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy
//This script is used to give the church health but also to detect if the enemy is in a its collider
//and if so the church will lose life

namespace Items
{
    public class Church : MonoBehaviour
    {

        [SerializeField]
        public int churchHealth;

        //Check if the enemy is near the church and if so loose life and destroy the enemy
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<AIScript>().DiePlease();
                churchHealth--;

                if (churchHealth <= 0)
                {
                    //GAME OVER!!
                }
            }
        }
    }
}
