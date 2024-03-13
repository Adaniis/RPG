
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
        {
            if (health.IsDead()) return;
            if (InterecatWhitCombat()) return;
            if (InterecatWithMouvement()) return;
        }

        private bool InterecatWhitCombat()
        {
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
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}