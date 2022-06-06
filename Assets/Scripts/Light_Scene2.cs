using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Scene2 : MonoBehaviour
{
    public Light Light2;

    private void Start()
    {
        Light2.intensity = 0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Light2.intensity = 10f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Light2.intensity = 0f;
    }
}
