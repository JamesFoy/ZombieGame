using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy
//This script is used to allow the enemies to damage the player when they are within a certain collider

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {

        [SerializeField]
        private int damage;

        private GameObject Player;

        [SerializeField]
        PlayerStats player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                player.Health -= damage;
            }

            if (player.Health <= 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}

