using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class mover : MonoBehaviour
{
    [SerializeField] Transform target;
    

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
     void MoveToCursor()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
         bool hasHit =   Physics.Raycast(ray,out hit);
            if (hasHit);
            {
                GetComponent<NavMeshAgent>().destination = hit.point;
            }
    } 

        

 
    }
}
