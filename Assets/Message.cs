using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public GameObject MyGameObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            MyGameObject.GetComponent<MeshRenderer>().enabled = true;
            MyGameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
