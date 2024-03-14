using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core
{
    public class followcamera : MonoBehaviour
    {
        // creer un chance dans l'inspector nommer target 
        [SerializeField] Transform target;
        void LateUpdate()
        {// MiniScript qui permet de dire que le porteur de celui ci son componnent transfomer = position de la cible (X,Y,Z)
            transform.position = target.position;
        }
    }

}
