using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joystick;
    public CameraFollow cameraFollow;
    public NameBar nameBar;

    private Vector3 mouseDir, moveDir;
    private Quaternion lookRotation;
    private bool move;
    private bool markEnabled;

    private void Start()
    {
        OnInit();
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    private void Update()
    {
        if (!isDead)
        {
            JoystickMove();
            AttackControl();
        }
    }

    private void JoystickMove()
    {
        mouseDir = joystick.mouseDir;

        if ((mouseDir - Vector3.zero).sqrMagnitude > Constant.ZERO)
        {
            move = true;
            CalMoveDir();
            Move();
            ChangeAnim(Constant.ANIM_RUN);
        }
        else
        {
            move = false;
            if (!isAttacking) ChangeAnim(Constant.ANIM_IDLE);
        }
    }

    private void CalMoveDir()
    {
        moveDir.x = mouseDir.x;
        moveDir.z = mouseDir.y;
        lookRotation = Quaternion.LookRotation(moveDir);
    }

    private void Move()
    {
        charTransform.position = Vector3.Lerp(charTransform.position, charTransform.position + moveDir, speed * Time.deltaTime);
        charTransform.rotation = Quaternion.Slerp(charTransform.rotation, lookRotation, speed * Time.deltaTime);
    }

    public override void RemoveTargetCollider(Collider other)
    {
        if (other == targetCollider) UnMarkTarget();
        base.RemoveTargetCollider(other);
    }

    private void MarkTarget()
    {
        markEnabled = true;
        (target as Enemy).EnableMark();
    }

    private void UnMarkTarget()
    {
        markEnabled = false;
        (target as Enemy).DisableMark();
    }

    private void AttackControl()
    {
        if (ScanTarget())
        {
            if (!markEnabled) MarkTarget();
            if (!move) Attack();
        }

        if (isAttacking)
        {
            if (move)
            {
                StopAttack();
            }
            else if (!ScanTarget())
            {
                StopAttack();
                ChangeAnim(Constant.ANIM_IDLE);
            }
        }
    }

    public override void HitTarget(Collider target)
    {
        base.HitTarget(target);
        cameraFollow.ScaleOffset();
    }

    public void SetName(string name)
    {
        nameBar.SetName(name);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Ins.EndLevel(false);
    }
}
