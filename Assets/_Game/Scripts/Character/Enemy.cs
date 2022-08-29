using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent navMeshAgent;
    public Mark mark;

    private const float MAX_X = 50f;
    private const float MAX_Z = 60f;

    private IState<Enemy> currentState;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Enemy> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void ResetAgentDes()
    {
        navMeshAgent.destination = charTransform.position;
    }

    public void Patrolling()
    {
        if (IsReachDes())
        {
            navMeshAgent.destination = RandomPoint(charTransform.position, 10f);
        }
    }

    public Vector3 RandomPoint(Vector3 center, float radius)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * radius;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }

    public bool IsReachDes()
    {
        bool isReachDes = !navMeshAgent.pathPending
            && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
            && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f);

        return isReachDes;
    }

    public void Targeted()
    {
        mark.EnableMark();
    }

    public override void OnDespawn()
    {
        GameObject.Destroy(this.gameObject);
    }
}
