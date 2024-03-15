using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.control
{
    public class IAcontroller : MonoBehaviour {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float supiscionTime = 5f;
        [SerializeField] Patrolling patrolpath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float PatrolSpeedFraction = 0.2f;

        Fighter fighter;
        GameObject player; 
        Health health;
        Vector3 guardPosition; 
        float timeSinceLastSawThePlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        float waitingTime = 0f;
        float timeSinceArriveWaypoint = 3f;

        //creation des variable fighter player et health baser rescpectivement sur les script fighter sur le tag player et sur le script health
        void Start ()
        {
            //attribution des valeurs au variable créer au dessus 
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position; 

        }
        private void Update()
        {//fonction d'attaque qui check l'etat du joueur avec IsDead
            //et verifier si il est dans le range d'attaque ET fait appele a la fonction CanAttack du script fighter 
            // si oui il prend le script fighter et lance la methode Attack supiscion ou Guard 
            if (health.IsDead()) return;  
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawThePlayer = 0;
                AttackBehaviours();
            }
            else if ( timeSinceLastSawThePlayer < supiscionTime)
            {
                SupiscionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }
            timeSinceLastSawThePlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            
            Vector3 nextPosition = guardPosition;

            if(patrolpath != null)
            {
                
                if (AtWaypoint())
                {
                    
                    waitingTime += Time.deltaTime;
                    
                    if (waitingTime > timeSinceArriveWaypoint )
                    {
                        CycleWaypoint();
                        waitingTime = 0f;
                    }
                    
                    
                }
                nextPosition = GetCurrentWaypoint();

            }
            GetComponent<mover>().StartMoveAction(nextPosition, PatrolSpeedFraction);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolpath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolpath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position,GetCurrentWaypoint());
        return  distanceToWaypoint < waypointTolerance;
        }

        private void SupiscionBehaviour()
        {
            GetComponent<Scheduler>().CancelCurrentlAction();
        }

        private void AttackBehaviours()
        {
            
            GetComponent<Fighter>().Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
           // fonction utilisée plus haut qui permer de commparer les postion du joueur avec Du pJ et qui retourne un true or false 
           // si la distance est inferieur a la distance de chase 
             float DistanceToPlayer =  Vector3.Distance(player.transform.position, transform.position);
            return DistanceToPlayer < chaseDistance;
        }

        // permet de faire apparaitre des Sphere avant meme de lancer le programme pour visualiser la distance d'attaque 
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }



    }
}