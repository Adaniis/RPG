using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematiqueTrigger : MonoBehaviour
{
    GameObject player; 
    bool hasTrigger = true;
    private void OnTriggerEnter(Collider other)
    {

        
            
        if (hasTrigger == true && other.gameObject.CompareTag("Player"))
        GetComponent<PlayableDirector>().Play();
        hasTrigger = false;
        
    }
}
