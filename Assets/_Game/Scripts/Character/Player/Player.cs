using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joystick;
    public CameraFollow cameraFollow;

    private Vector3 mouseDir, moveDir;
    private Quaternion lookRotation;
    private bool isStop = true;

    private void Start()
    {
        OnInit();
        ChangeAnim(Constant.ANIM_IDLE);
    }

    private void Update()
    {
        if (!dead)
        {
            JoystickMove();
            AttackControl();
        }
    }

    private void JoystickMove()
    {
        mouseDir = joystick.mouseDir;

        if ((mouseDir - Vector3.zero).sqrMagnitude > 0.01f)
        {
            isStop = false;
            CalculateMoveDir();
            Move();
            ChangeAnim(Constant.ANIM_RUN);
        }
        else
        {
            isStop = true;
            if (!isAttacking) ChangeAnim(Constant.ANIM_IDLE);
        }
    }

    private void CalculateMoveDir()
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

    private void AttackControl()
    {
        if (DetectEnemy())
        {
            MarkEnemy();
            if (isStop) Attack();
        }

        if (isAttacking)
        {
            if (!isStop || !DetectEnemy())
            {
                StopAttack();
            }
        }
    }

    private void MarkEnemy()
    {
        Enemy enemy = Cache<Enemy>.Get(targetCollider);
        enemy.Targeted();
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void KillAnEnemy()
    {
        base.KillAnEnemy();
        cameraFollow.ScaleOffset();
    }
}
