using RPG.control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class NewBehaviourScript : MonoBehaviour
{

   

    private void Start()
    {
        
        GetComponent<PlayableDirector>().played += disableControle;
        GetComponent<PlayableDirector> ().stopped += returnControle;

           
    }
    void disableControle(PlayableDirector pd)
    {
        GameObject player = GameObject.FindWithTag("Player");

        player.GetComponent<Scheduler>().CancelCurrentlAction();    
        player.GetComponent<playercontroller>().enabled = false;
        print("disableControle");
        
    }

    void returnControle(PlayableDirector pd)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<playercontroller>().enabled = true;
        print("return");
    }
}