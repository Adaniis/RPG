using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healtpoint = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        
        public void Takedamage(float damage)
        {
            healtpoint = Mathf.Max(healtpoint - damage,0);
            print(healtpoint);
            if (healtpoint == 0)
            {
                Die();
                 
            }
        }

        private void Die()
        {if (isDead) return;
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<Scheduler>().CancelCurrentlAction();   
            }
            
        }
    }
}   
