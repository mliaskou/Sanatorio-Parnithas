using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScene2 : MonoBehaviour
{

    public GameObject Message;
    public Light LightSource;
    private bool LightIsOn;
    public float fadeSpeed = 2f;
    public float highIntensity = 4f;
    public float lowIntensity = 0.0f;
    private float targetIntensity;
    public float change = 0.2f;

    public GameObject particle1;
    public GameObject particle2;

    void Awake()
    {
        LightSource.intensity = 0f;
        targetIntensity = highIntensity;
    }

    private void Start()
    {
        particle1.SetActive(false);
        particle2.SetActive(false);
    }
    public void Update()
    {
        if (LightIsOn == false)
        {
            LightSource.intensity = Mathf.Lerp(LightSource.intensity, 0f, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
            particle1.SetActive(true);
            particle2.SetActive(true);
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
            TestMessage(true);
        }
    }

    private void TestMessage(bool state)
    {
        Message.SetActive(state);
        LightIsOn = state;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TestMessage(false);
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
