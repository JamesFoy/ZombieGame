using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour {

    [SerializeField]
    public UIScript uiScript;

    private Animator anim;
    [SerializeField]
    private bool isDead;
    private bool isMoving;

    [SerializeField]
    public EnemyStats enemy;

    public Transform player;
    public int chaseDistance;
    public int findDistance;


    UnityEngine.AI.NavMeshAgent agent;
    public GameObject[] points;
    public int destPoint = 0;

    public enum Behaviours { Patrol, Combat, Dead };
    public Behaviours currBehaviour = Behaviours.Patrol;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        points = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    void Update()
    {
        RunBehaviours();

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isDead", isDead);

        if (enemy.Health <= 0)
        {
            Debug.Log("Dead");
            isDead = true;
            currBehaviour = Behaviours.Dead;
        }
        else
        {
            isDead = false;
        }
    }

    void RunBehaviours()
    {
        switch (currBehaviour)
        {
            case Behaviours.Patrol:
                RunPatrolState();
                break;
            case Behaviours.Combat:
                RunCombatState();
                break;
            case Behaviours.Dead:
                RunDeadState();
                break;
        }
    }

    void RunDeadState()
    {
        if (isDead == true)
        {
            uiScript.score += 100;
            currBehaviour = Behaviours.Dead;
            agent.SetDestination(this.transform.position);
            isMoving = false;
            Destroy(this.gameObject);
        }
    }

    private void RunCombatState()
    {
        isMoving = true;
        if (Vector3.Distance(transform.position, player.position) > chaseDistance)
        {
            currBehaviour = Behaviours.Patrol;
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    private void RunPatrolState()
    {
        isMoving = true;
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            currBehaviour = Behaviours.Combat;
        }
        else if (agent.remainingDistance < 0.5f)
        {
            if (points.Length == 0)
            {
                return;
            }
            agent.SetDestination(points[destPoint].transform.position);
            //if goes higher than the total number of waypoints -> go back to start of array
            destPoint = (destPoint + 1) % points.Length;
        }
    }
}
