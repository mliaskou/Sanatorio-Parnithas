using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : MonoBehaviour
{
    public GameObject player;
    public GameObject position2;
    Animator anim;
    NavMeshAgent nma;
    public Transform[] targets;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        var currentIndex = GetCurrentTargetIndex();
        if (currentIndex.HasValue)
        {
            var nextTargetIndex = currentIndex.Value + 1;
            if (nextTargetIndex >= targets.Length)
            {
                nextTargetIndex = 0;
            }
            var nextTarget = targets[nextTargetIndex];
            nma.SetDestination(nextTarget.transform.position);
        }
    }

    int? GetCurrentTargetIndex()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            var target = targets[i];
            var distance = Vector3.Distance(target.position, transform.position);
            if (distance < 0.25f)
            {
                return i;
            }
        }
        return null;
    }
    /*
        void Start()
        {

            anim = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            //nma.updatePosition = false;
            anim.applyRootMotion = true;

        }

        void Update()
        {
            nma.SetDestination(player.transform.position);
            var distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < 0.25f)
            {
                nma.isStopped = true;
            }
        }*/
}



