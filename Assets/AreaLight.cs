using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLight : MonoBehaviour
{
    // Start is called before the first frame update
    public Light Light2;
   



    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player"))
        {
            Light2.intensity = 18f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Light2.intensity = 0f;

    }
}
