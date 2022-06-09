using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : MonoBehaviour
{

    NavMeshAgent nma;
    public GameObject player;
    Animator anim;


    private void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        nma.updatePosition = false;
        nma.updateRotation = true;
    }


    private void Update()
    {
        nma.SetDestination(player.transform.position);
        anim.SetFloat("Speed", 1f);




             float distance = Vector3.Distance(nma.destination, player.transform.position);

        if (distance > 1f)
        {
            // Play walk anim
            anim.SetFloat("SpeedY", 1);
        }
        else
        {
            // Play idle anim
            anim.SetFloat("SpeedY", 0);
        }

        nma.nextPosition = anim.rootPosition;
    }

    /* [RequireComponent(typeof(Animator))]

    public class RootMotionScript : MonoBehaviour
    {

        void OnAnimatorMove()
        {
            Animator animator = GetComponent<Animator>();

            if (animator)
            {
                Vector3 newPosition = transform.position;
                newPosition.z += animator.GetFloat("Runspeed") * Time.deltaTime;
                transform.position = newPosition;
            }
        }

    }*/
}



