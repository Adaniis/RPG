using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.control
{

    public class Patrolling : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f; // variable pour la taille des spheres

        private void OnDrawGizmos()
        {
            // fonction permetant de dessiner une sphere par waypoint et de tirer un ligne entre les waypoint 
            //car pour chaque i plus petit que le nombre d'enfant on fait un tour dans la boucle et on increment I 
            // on dit que le variable J = i 
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }
        // fonction qui permet de verifier vers qui la droite vas etre tracer en fessant i+1 si c'est egale au nombre total de child retourn 
        // un Zero pour permettre de fermer la boucle 
        public  int GetNextIndex(int i)
        {
            if (i + 1
                == transform.childCount) return 0; 
            return i + 1;
        }
        // fonction qui prend la valeur entiere de I et qui en retourne la position TriDi
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}

