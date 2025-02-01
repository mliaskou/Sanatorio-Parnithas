using UnityEngine;
using UnityEngine.AI;

public class ghost_move : MonoBehaviour
{
    public Transform[] targets;
    NavMeshAgent nma;
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
}











