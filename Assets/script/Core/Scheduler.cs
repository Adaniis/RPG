using RPG.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Scheduler : MonoBehaviour
    {
        IAction currentAction; 
       public void StartAction(IAction action)
        {//fonction public qui permet de voir si currentAction == action si oui sort de la boucle,
         //si la current action est different de null appelle la fonction cancel
         // et set la currentAction sur la valeur d'action qui a été passer en parametre 

            if (currentAction == action) return;

            if (currentAction != null) {
                currentAction.cancel();    
            }
            currentAction = action;

        }
        public void CancelCurrentlAction()
        {
            //fonciton qui permet de faire renter en parametre la valeur null dans StartAction
            StartAction(null);
        }


    }
}
