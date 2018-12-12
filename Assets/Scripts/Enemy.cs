using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    private NavMeshAgent enemyNav;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private int damage;

    [SerializeField]
    PlayerStats player;

    [SerializeField]
    public EnemyStats enemy;

    private Animator anim;

    [SerializeField]
    private bool isDead;

    private bool isMoving;

	// Use this for initialization
	void Start ()
    {
        isMoving = false;
        enemyNav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.Health -= damage; 
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (isDead == false)
        {
            enemyNav.SetDestination(target.transform.position);
            isMoving = true;
        }
        else
        {
            enemyNav.SetDestination(this.gameObject.transform.position);
            isMoving = false;
        }

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isDead", isDead);

        if (enemy.Health <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }
}
