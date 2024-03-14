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
        //creer un chance dans l'inspector nommer target
        [SerializeField] Transform target;
        // creer un variable navMeshAgent se typent sur le compossent NavMeshAgent
        NavMeshAgent navMeshAgent;
        Health health;
        void Start()
        {
            health = GetComponent<Health>();
            //on attribue les Valeur du NavMeshAgent dans la variable 
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        //fonction public appeler StarMoveAction avec en parametre une variable de type Vector3(X,Y,Z) nommer desination 
        // on vas chercher le script Scheduler plus precissement le methode StartAction et on lui passe la valeur this
        //pour anuller les autres animation et ensuite on demande la methode MoveTo et on lui passe ariable desination 

        public void StartMoveAction(Vector3 desination)
        {
            GetComponent<Scheduler>().StartAction(this);
            
            MoveTo(desination);
        }
        void Update()
        {
            //vas déactiver le NavMesh si IsDead is true 
            navMeshAgent.enabled = !health.IsDead();
            //methode expliquée plus bas
            UpdateAnimation();
        }

        public void cancel()
        {// fonction cancel qui indique au nasMeshAgent de s'arreter 
            navMeshAgent.isStopped = true;
        }

        //fonction public MoveTo avec une variable en vector 3 qui a faire que la variable destionation prend les coordonée 
        // du navMeshAgent et qui vas indiquée au NavMesh de bien se déplacée
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        private void UpdateAnimation()
        {
            //fonction de déplacement qui vas checher la valeur de velocitee dans le NavMesh pour l'introduire dans une variable 
            //qui vas etre convertie en local pour ensuite en extraire que la valeur Z qui est stockée dans un Variable Float speed 
            // ensuite on fait appelle au compossant animator et au trigger Forward speed en lui donnant en valeur de référence la vitesse Z
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

      

    }

        

 
    
}
