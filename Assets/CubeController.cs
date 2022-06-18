using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Transform targetPosition;
    public Transform originalPosition;
    public float speed1 = 2;
    public float speed2 = 2;
    public float smoothTime = 0.5f;
    private bool isMovingBack = false;

    Vector3 velocity;

    private void Start()
    {
        
    }

    private void Update()
    {
        var distance = Vector3.Distance(targetPosition.transform.position, transform.position);
        if (transform.position !=targetPosition.transform.position)
        {
            var change1 = speed1 * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, change1);
        }
        if (Vector3.Distance(transform.position, targetPosition.transform.position) < 0.001f)
        {
            isMovingBack = true;
        }

        if(isMovingBack== true)
        {
            transform.position -= (transform.position - originalPosition.transform.position).normalized * speed2 * Time.deltaTime;
        }
    }


    
}
