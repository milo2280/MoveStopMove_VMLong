using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent navMeshAgent;
    public CapsuleCollider capsuleCollider;
    public Mark mark;

    private IState<Enemy> currentState;

    private const float DECOMPOSE_TIME = 2f;

    private void Start()
    {
        OnInit();
    }

    void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }

    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Ins.AEnemyDead(this);
        ResetAgentDes();
        StartCoroutine(IEDecompose());
    }

    protected IEnumerator IEDecompose()
    {
        yield return new WaitForSeconds(DECOMPOSE_TIME);

        OnDespawn();
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
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

    public void EnableMark()
    {
        mark.EnableMark();
    }

    public void DisableMark()
    {
        mark.DisableMark();
    }
}
