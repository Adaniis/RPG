using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcamera : MonoBehaviour
{
    [SerializeField] Transform target;
    void Update()
    {
        transform.position = target.position;
    }
}