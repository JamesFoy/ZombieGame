using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

//Author - James Foy, Simon Hunt
//This script was used by the enemies in the game to correctly track the states that the enemies could be in
//THis would contain tracks like patroling, combat and if they are dead. This also updates some UI and varibales on other scripts

namespace Enemy
{
    public class AIScript : MonoBehaviour
    {

        public event Action<AIScript> HaveDied;

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


        //This is used to set the correct componets to certain variables so they can be used later
        void Start()
        {
            uiScript = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
            anim = GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            points = GameObject.FindGameObjectsWithTag("Waypoint");
        }

        //This update function is used to run the different states during the game
        void Update()
        {
            //This is calling the method that starts the behaviours
            RunBehaviours();

            //Setting the animation controller to the booleans in script
            anim.SetBool("isMoving", isMoving);
            anim.SetBool("isDead", isDead);

            if (enemy.Health <= 0)
            {
                isDead = true;
                currBehaviour = Behaviours.Dead;
                if (HaveDied != null)
                {
                    HaveDied.Invoke(this);
                }
            }
            else
            {
                isDead = false;
            }
        }

        //This runs all of the different behaviours that the enemy can be in, this is also constantly run and the enemy
        //is able to change the state at any time
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

        //This controls the behaviours that will happen when the enemy state is dead
        void RunDeadState()
        {
            if (isDead == true)
            {
                uiScript.score += 100;
                currBehaviour = Behaviours.Dead;
                agent.SetDestination(this.transform.position);
                isMoving = false;
                new WaitForSeconds(3.0f);
                Destroy(this.gameObject);
            }
        }

        //This is used to make sure that enemies die in other scripts with a simple function
        public void DiePlease()
        {
            enemy.Health = 0;
        }

        //This controls all of the behaviour that will run when the enemies is in the combat state
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

        //This controls all of the behaviour that will run when the enemies is in the patrol state
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
}
