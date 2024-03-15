using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        print("disable");
    }

    void returnControle(PlayableDirector pd)
    {
        print("return");
    }
}