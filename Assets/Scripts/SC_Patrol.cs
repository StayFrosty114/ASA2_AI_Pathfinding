using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_Patrol : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent patrolAgent;


    void Start()
    {
        // Captures a reference to the NavMesh component of the agent.
        patrolAgent = GetComponent<NavMeshAgent>();

        // Starting patrol route.
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up.
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        patrolAgent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets close to the current one.
        if (!patrolAgent.pathPending && patrolAgent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
