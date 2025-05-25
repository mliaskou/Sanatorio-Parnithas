using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTrigger: MonoBehaviour
{
    public AudioSource sanatorio_sound;
    

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            sanatorio_sound.Play();
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sanatorio_sound.Stop();

        }
        
    }

}
