using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.ChangeAnim(Constant.ANIM_IDLE);
    }

    public void OnExecute(Enemy t)
    {
        if (t.DetectEnemy())
        {
            t.Attack();
        }
        else
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy t)
    {
        t.StopAttack();
    }
}
