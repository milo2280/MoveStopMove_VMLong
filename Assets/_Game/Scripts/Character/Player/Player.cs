using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joystick;
    public CameraFollow cameraFollow;

    private Vector3 mouseDir, moveDir;
    private Quaternion lookRotation;
    private bool isMoving;
    private bool isMarkEnabled;

    private void Start()
    {
        OnInit();
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public override void OnInit()
    {
        base.OnInit();
        isMarkEnabled = false;
        myTransform.position = Vector3.zero;
        myTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
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
            isMoving = true;
            CalculateMoveDir();
            Move();
            ChangeAnim(Constant.ANIM_RUN);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            ChangeAnim(Constant.ANIM_IDLE);
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
        myTransform.position = Vector3.Lerp(myTransform.position, myTransform.position + moveDir, speed * Time.deltaTime);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, speed * Time.deltaTime);
    }

    public override void RemoveTargetCollider(Collider other)
    {
        if (other == targetCollider) UnMarkTarget();
        base.RemoveTargetCollider(other);
    }

    private void MarkTarget()
    {
        isMarkEnabled = true;
        (target as Enemy).EnableMark();
    }

    private void UnMarkTarget()
    {
        isMarkEnabled = false;
        (target as Enemy).DisableMark();
    }

    private void AttackControl()
    {
        if (ScanTarget())
        {
            if (!isMarkEnabled) MarkTarget();
            if (!isMoving) Attack();
        }
        
        if (isAttacking)
        {
            Attacking();

            if (isMoving)
            {
                StopAttack();
            }
        }

        if (isDelaying)
        {
            Delaying();
        }
    }

    public override void OnKill()
    {
        base.OnKill();
        cameraFollow.ScaleOffset();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Ins.EndLevel(false);
    }
}
