using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4 : MonoBehaviour
{
    public GameObject Message;
    public Light LightSource;
    private bool LightIsOn;
    public float fadeSpeed = 2f;
    public float highIntensity = 4f;
    public float lowIntensity = 0.0f;
    private float targetIntensity;
    public float change = 0.2f;


    void Awake()
    {
        LightSource.intensity = 0f;
        targetIntensity = highIntensity;
    }
    public void Update()
    {
        if (LightIsOn == false)
        {
            LightSource.intensity = Mathf.Lerp(LightSource.intensity, 0f, fadeSpeed * Time.deltaTime);

            CheckTargetIntensity();
        }
        else
        {
            LightSource.intensity = Mathf.Lerp(LightSource.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Message.SetActive(true);
            LightIsOn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Message.SetActive(false);
            LightIsOn = false;
        }

    }

    void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity - LightSource.intensity) < change)
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

