using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent navMeshAgent;
    public CapsuleCollider capsuleCollider;
    public Mark mark;
    public SkinnedMeshRenderer bodyMesh, pantMesh;

    public Color color;

    private IState<Enemy> currentState;
    private Indicator indicator;

    private const float DECOMPOSE_TIME = 2f;

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
        SetName(DataManager.Ins.GetRandomName());
        color = DataManager.Ins.GetRandomColor();
        bodyMesh.material.color = color;
        pantMesh.material.color = color;
        nameBar.SetColor(color);
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
        navMeshAgent.destination = myTransform.position;
    }

    public void Patrolling()
    {
        if (IsReachDes())
        {
            navMeshAgent.destination = RandomPoint(myTransform.position, 10f);
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

    public void AssignIndicator(Indicator indicator)
    {
        this.indicator = indicator;
        indicator.SetColor(color);
        indicator.ChangeScore(score);
    }

    public override void ChangeScore(int point)
    {
        base.ChangeScore(point);

        if (indicator != null)
        {
            indicator.ChangeScore(point);
        }
    }
}
