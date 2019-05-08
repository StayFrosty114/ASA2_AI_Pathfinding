using UnityEngine;
using UnityEngine.AI;
using System;

public class SC_Chase : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float destinationMoveRecalculateDistance = 1.0f;

    private NavMeshAgent agent;

    private Action CurrentStateUpdate;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        CurrentStateUpdate = SearchUpdate;
    }

    private void Update()
    {
        CurrentStateUpdate();
    }

    private void SearchUpdate()
    {
        if (Vector3.Distance(target.position, agent.destination) > destinationMoveRecalculateDistance)
        {
            agent.SetDestination(target.position);
        }
    }
}