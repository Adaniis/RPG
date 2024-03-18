using RPG.Combat;
using RPG.core;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttack = 1f ;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity; 

        // on creer trois variable modifiable depuis l'inspector et on import le script health qu'on vas stocker dans la variable target 
        // on crrer aussi un variable timeSinceLastAttack = infini pour ne pas avoir de delay avant la premier attaque apres avoir lancer le jeu 
        private void Update()
        {
            //incremente le variable pour le delay entre les attaque 
            timeSinceLastAttack += Time.deltaTime;

            // si la target et null ou est mort on saute le block 
            //sinon si elle n'est pas a porter IsInRange on fait appelle au script mover
            //et  a sa fonction MoveTo avec en paramettre la position X,Y,Z de l'ennemie 
            //sinon a fait appel au cancel qui indique au navMeshagent false et on fait appel a la fonction AttackBehaviour
            if (target == null) return;
            if(target.IsDead()) return;   
            if (!IsInRange())
            {
                GetComponent<mover>().MoveTo(target.transform.position,1f);

            }
            else
            {

                GetComponent<mover>().cancel();
                
                AttackBehaviour();
                
            }

        }

        private void AttackBehaviour()
        {
            //fonction d'attaque qui en premie instant fait notre compossant transform "regarde"l'ennemie avec la fonction build-in
            //LookAt qui prend en parametre la cible et c'est coordonée 
            // ensuite si le delay d'autoattack est respectée on appeller la fonction triggerAttack et on reset le delai
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttack)
            {
                triggerAttack();

                timeSinceLastAttack = 0f;

            }

        }

        private void triggerAttack()
        {//fonction qui appel l'animator qui suprimer si besoin le trigger de la l'animatio stop attack et qui appel celui de l'attack 
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        void Hit()
        {//fonction qui fait appel au hit event dans l'animator et qui indique que si la target est Null on saute le block sinon on lance
         //la methode de Takedamage
            if (target == null) return; 
            target.Takedamage(weaponDamage);
        }

        private bool IsInRange()
        {// fonction qui permet de faire un return en Bool si la distance entre les coordonée du player et du Pnj sont bien inferieur a la valeur du weaponRange
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        public bool CanAttack(GameObject combatTarget)
        { //fonction bool qui prend en parametre un variable de type GameObject nommer combatTarget qui lui provient du player controller 
          //GameObject targetGameObject = target.gameObject;  
           //f (!GetComponent<Fighter>().CanAttack(target.gameObject))
           //si le combatTarget == null on saute le block 
           // ensuite on creer un variable targeToTest qui vas rechercher le script Health de la cible 
           // on retune le targetToTest true si il est n'est pas null et si IsDead est false 
                if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }


        public void Attack(GameObject combatTarget)
        {
            //fonction public utiliser dans player controller qui prende le game object en parametre, qui vas indiquée au scheduler 
            //d'arreter l'animation en cours de de lancer celle si et créer une variable target qui se base sur le script health du gameObject
            GetComponent<Scheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }

        public void cancel()
        {
            //fonction d'annulation d'attaque 
            StopAttack();
            target = null;
            GetComponent<mover>().cancel();
        }

        private void StopAttack()
        {
            //fonction de reset des trigger dans l'animator 
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
