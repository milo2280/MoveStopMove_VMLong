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
    private Indicator indicator;

    private const float DECOMPOSE_TIME = 2f;

    private void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        SetColor(DataManager.Ins.GetRandomColor());
        weapon = hand.OnInit(WeaponManager.Ins.GetRandomType());
        SetName(DataManager.Ins.GetRandomName());
        ChangeState(new IdleState());
        EquipSkin(SkinManager.Ins.GetRandomHair());
        EquipSkin(SkinManager.Ins.GetRandomPant());
        
        base.OnInit();

        if (LevelManager.Ins.playerScore > 0)
        {
            score = LevelManager.Ins.playerScore + Random.Range(-5, 5);
            if (score < 0) score = 0;
            UpdateScore(score);
            for (int i = 0; i < Mathf.FloorToInt(score / 5); i++)
            {
                IncreaseSize();
            }
        }
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

    public void AttackControl()
    {
        if (ScanTarget())
        {
            Attack();

            if (isAttacking)
            {
                Attacking();
            }

            if (isDelaying)
            {
                Delaying();
            }
        }
        else
        {
            ChangeState(new PatrolState());
        }
    }

    public void ResetAgentDes()
    {
        navMeshAgent.destination = m_Transform.position;
    }

    public void Patrolling()
    {
        if (IsReachDes())
        {
            navMeshAgent.destination = RandomPoint(m_Transform.position, 10f);
        }
    }

    public bool IsReachDes()
    {
        bool isReachDes = !navMeshAgent.pathPending
            && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
            && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f);

        return isReachDes;
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
        indicator.UpdateScore(score);
    }

    public override void UpdateScore(int point)
    {
        base.UpdateScore(point);

        if (indicator != null)
        {
            indicator.UpdateScore(point);
        }
    }
}
