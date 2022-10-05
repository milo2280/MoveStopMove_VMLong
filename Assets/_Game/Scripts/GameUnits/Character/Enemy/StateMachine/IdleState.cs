using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    private float t1, t2;
    private float patrolT, attackT;

    public void OnEnter(Enemy t)
    {
        t1 = t2 = 0f;
        patrolT = Random.Range(1f, 2f);
        attackT = Random.Range(0f, 4f);
        t.ChangeAnim(Constant.ANIM_IDLE);
    }

    public void OnExecute(Enemy t)
    {
        if (!GameManager.Ins.IsState(GameState.MainMenu))
        {
            t1 += Time.deltaTime;
            if (t1 > patrolT)
            {
                t.ChangeState(new PatrolState());
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
    }

    public void OnExit(Enemy t)
    {

    }
}
