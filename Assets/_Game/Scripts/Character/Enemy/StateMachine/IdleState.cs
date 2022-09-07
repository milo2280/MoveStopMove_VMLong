using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    private float timer;
    private float endTime;

    public void OnEnter(Enemy t)
    {
        timer = 0f;
        endTime = Random.Range(1f, 2f);
        t.ChangeAnim(Constant.ANIM_IDLE);
    }

    public void OnExecute(Enemy t)
    {
        if (!GameManager.Ins.IsState(GameState.MainMenu))
        {
            timer += Time.deltaTime;
            if (timer > endTime)
            {
                t.ChangeState(new PatrolState());
            }

            if (t.ScanTarget())
            {
                t.ChangeState(new AttackState());
            }
        }
    }

    public void OnExit(Enemy t)
    {

    }
}
