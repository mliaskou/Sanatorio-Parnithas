using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light3 : MonoBehaviour
{

    public Light Spotlight;
    // Start is called before the first frame update
    void Start()
    {
        Spotlight.intensity = 0f;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Spotlight.intensity = 3f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
      if(other.CompareTag("Player"))
        {
            Spotlight.intensity = 0f;
        }

    }
}
