using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerMove : MonoBehaviour
{
    //public Transform[] targets;
    NavMeshAgent nma;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, GameStateManager._Instance.player.transform.position);
        if (distance > 10)
        {
            Debug.LogError("distance bigger than 10");
            nma.SetDestination(GameStateManager._Instance.player.transform.position);
        }
        else
        {
            if (nma.remainingDistance <= nma.stoppingDistance)
            {
                Debug.LogError("distance less than 10");
                nma.stoppingDistance = 10f;
            }
        }
    }
}
