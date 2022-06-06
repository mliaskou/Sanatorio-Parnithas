using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioSource ExitSound;
    public static bool AlreadyPlayed;


    void Start()
    {
        AlreadyPlayed = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !AlreadyPlayed)
        {
            ExitSound.Play();
            AlreadyPlayed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ExitSound.Stop();
        }
    }
}
