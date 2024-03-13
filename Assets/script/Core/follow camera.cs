using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core
{
    public class followcamera : MonoBehaviour
    {
        [SerializeField] Transform target;
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }

}
