
using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace RPG.control
{
    public class playercontroller : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {//verifier si une des 3 fonction est en cours et saute le bloc si oui
            if (health.IsDead()) return;
            if (InterecatWhitCombat()) return;
            if (InterecatWithMouvement()) return;
        }

        private bool InterecatWhitCombat()
        {
            // pour chaque itération du foreach qui se base sur le nombre de Raycast qui on Hit on vas chercher
            //le composant CombatTarget qu'on attribue a la variable Target si cette variable est nul on continue la boucle
            //foreach on on vas rechercher le gameobject de la variable target qui est mis dans un Variable targetGameObject
            //de type GameObject
            // ensuite in vas verifier si il peut attaquer et si il fait clique gauche on lance la fonction d'attaque 
            RaycastHit[] Hits = Physics.RaycastAll(GetRay());
            foreach (RaycastHit hit in Hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                GameObject targetGameObject = target.gameObject;  
                    if (!GetComponent<Fighter>().CanAttack(target.gameObject)) 
                {
                    continue;
                } 

                    if (Input.GetMouseButtonDown(0)) {

                    GetComponent<Fighter>().Attack(target.gameObject);
                     }
                return true;

            }
            return false;
        }   

        private bool InterecatWithMouvement()
        {
        // on definie un Variable Hit de type RaycastHit, on retourne une valeur Bool dans la variable hasHit = si le ray tire par
        // la camera retourne une valeur
        // si oui et si on fait clique gauche on vas chercher dans le script mover la fonction StartMoveAction avec les valeur hit.point qui correspond au coordonée tridimmensionnel 
    
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRay()
        {
            //fonction qui permet de faire tirer un ray sur la postion de la souris 
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}