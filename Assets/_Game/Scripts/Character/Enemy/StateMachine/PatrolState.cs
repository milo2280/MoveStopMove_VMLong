using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{
    private float t1, t2;
    private float idleT, attackT;

    public void OnEnter(Enemy t)
    {
        t1 = t2 = 0f;
        idleT = Random.Range(50f, 60f);
        attackT = Random.Range(0f, 4f);

        t.ChangeAnim(Constant.ANIM_RUN);
    }

    public void OnExecute(Enemy t)
    {
        t.Patrolling();

        t1 += Time.deltaTime;
        if (t1 > idleT)
        {
            t.ChangeState(new IdleState());
        }


        if (t.ScanTarget())
        {
            //t2 += Time.deltaTime;
            //if (t2 > attackT)
            //{
                t.ChangeState(new AttackState());
            //}
        }
        else
        {
            t2 = 0f;
        }
    }

    public void OnExit(Enemy t)
    {
        t.ResetAgentDes();
    }
}
