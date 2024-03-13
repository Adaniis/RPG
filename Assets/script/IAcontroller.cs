using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.control
{
    public class IAcontroller : MonoBehaviour {
        [SerializeField] float chaseDistance = 5f;
        Fighter fighter;
        GameObject player; 
        Health health;
        void Start ()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }
        private void Update()
        {
            if (health.IsDead()) return;  
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                GetComponent<Fighter>().Attack(player);
            }
            else
            {
                fighter.cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
           
             float DistanceToPlayer =  Vector3.Distance(player.transform.position, transform.position);
            return DistanceToPlayer < chaseDistance;
        }
    }
}