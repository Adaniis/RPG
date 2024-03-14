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
            //retourne la valeur IsDead en valeur public pour tous les scirpts
            return isDead;
        }
        
        public void Takedamage(float damage)
        {
            //premier fonction utilise dans le script qui permet de calculer la décrémenation des Pv et les bloque a  0 quand il arrive a Zero 
            // trigger la fonction Die 
            healtpoint = Mathf.Max(healtpoint - damage,0);
            print(healtpoint);
            if (healtpoint == 0)
            {
                Die();
                 
            }
        }

        private void Die()
        {//fonction qui verifie si la cible est déja morte et si non change la variable en IsDead = true et lance l'animation de mort tous en 
            // annulant tous autre action &&animation
            if (isDead) return;
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<Scheduler>().CancelCurrentlAction();   
            }
            
        }
    }
}   
