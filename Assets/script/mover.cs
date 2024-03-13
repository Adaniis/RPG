using RPG.Combat;
using RPG.core;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class mover : MonoBehaviour,IAction
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void StartMoveAction(Vector3 desination)
        {
            GetComponent<Scheduler>().StartAction(this);
            
            MoveTo(desination);
        }
        void Update()
        {

            UpdateAnimation();
        }

        public void cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

      

    }

        

 
    
}
