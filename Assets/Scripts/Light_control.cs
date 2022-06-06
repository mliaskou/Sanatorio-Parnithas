using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_control : MonoBehaviour
{
    public Light sanatorio_Light;

    // Start is called before the first frame update
    void Start()
    {
        sanatorio_Light.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            sanatorio_Light.intensity = 18f;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sanatorio_Light.intensity = 0f;

        }

    }

}
