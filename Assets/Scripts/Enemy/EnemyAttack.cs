using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

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
