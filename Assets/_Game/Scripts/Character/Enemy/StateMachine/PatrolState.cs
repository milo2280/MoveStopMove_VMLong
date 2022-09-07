using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{
    private float timer;
    private float endTime;

    public void OnEnter(Enemy t)
    {
        timer = 0f;
        endTime = Random.Range(50f, 60f);

        t.ChangeAnim(Constant.ANIM_RUN);
    }

    public void OnExecute(Enemy t)
    {
        timer += Time.deltaTime;
        if (timer > endTime)
        {
            t.ChangeState(new IdleState());
        }

        t.Patrolling();

        if (t.ScanTarget())
        {
            t.ChangeState(new AttackState());
        }
    }

    public void OnExit(Enemy t)
    {
        t.ResetAgentDes();
    }
}
