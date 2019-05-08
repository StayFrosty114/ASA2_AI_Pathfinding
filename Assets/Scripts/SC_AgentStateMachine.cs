using UnityEngine;
using UnityEngine.AI;
using System;

public class SC_AgentStateMachine : SC_StateMachine
{
    private enum PlayerStates { Patrolling, Chasing }

    private NavMeshAgent agent;

    [SerializeField]
    Transform target;

    [SerializeField]
    float destinationMoveRecalculateDistance = 1.0f;

    // Patrol Points
    public Transform[] points;
    private int destPoint = 0;

    private Action CurrentStateUpdate;

    void Start()
    {
        // Captures a reference to the NavMeshAgent.
        agent = GetComponent<NavMeshAgent>();

        // Sets state to Patrol by default.
        CurrentStateUpdate = PatrolUpdate;
    }

    private void Update()
    {
        CurrentStateUpdate();
    }

    // Chasing State
    private void ChaseUpdate()
    {
       if (Vector3.Distance(target.position, agent.destination) > destinationMoveRecalculateDistance)
       {
           agent.SetDestination(target.position);
       }
    }

    // Patrolling State
    private void PatrolUpdate()
    {
        // Choose the next destination point when the agent gets close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    private void GotoNextPoint()
    {
        // Returns if no points have been set up.
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination, cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    // Initiate Chase State when player enters range.
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            Debug.Log("Chasing");
            agent.speed = 9.0f;
            CurrentStateUpdate = ChaseUpdate;
        }
    }

    // Return to Patrol State when player exits range.
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            Debug.Log("Patrolling");
            agent.speed = 6.0f;
            CurrentStateUpdate = PatrolUpdate;
        }
    }

    
}
