using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Sound : MonoBehaviour
{
    public AudioSource gsound;
    private bool isPlaying;

    public float fadeSpeed = 2f;
    public float highIntensity = 4f;
    public float lowIntensity = 0.0f;
    private float targetIntensity;
    public float change = 0.2f;

    private void Awake()
    {
        gsound.volume = 0f;
        targetIntensity = highIntensity;
    }
    private void Update()
    {

        if (isPlaying == false)
        {
            gsound.volume = Mathf.Lerp(gsound.volume, 0f, fadeSpeed * Time.deltaTime);

            CheckTargetIntensity();
        }
        else
        {
            gsound.volume = Mathf.Lerp(gsound.volume, targetIntensity, fadeSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gsound.Play();
            isPlaying = true;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gsound.Stop();
            isPlaying = false;
        }
    }
    void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity - gsound.volume) < change)
        {
            if (targetIntensity == highIntensity)
            {
                targetIntensity = lowIntensity;
            }
            else
            {
                targetIntensity = highIntensity;
            }
        }
    }

}
