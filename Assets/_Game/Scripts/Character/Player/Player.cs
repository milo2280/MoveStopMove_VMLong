using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joystick;
    public CameraFollow cameraFollow;

    public float gold;

    private Vector3 mouseDir, moveDir;
    private Quaternion lookRotation;
    private bool isMoving;
    private bool isMarkEnabled;
    private bool isWin;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (!isDead && !isWin)
        {
            JoystickMove();
            AttackControl();
        }
    }

    public override void OnInit()
    {
        isWin = false;
        isMarkEnabled = false;
        SetColor(Color.yellow);
        ChangeAnim(Constant.ANIM_IDLE);
        weapon = hand.OnInit(PlayerData.Ins.weaponType);
        m_Transform.position = Vector3.zero;
        m_Transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        base.OnInit();
    }

    public override void CalculateNewStat()
    {
        base.CalculateNewStat();
        gold = (gold + addBuff[(int)BuffType.Gold]) * (1 + percentBuff[(int)BuffType.Gold] / 100);
    }

    public void OnRevive()
    {
        isDead = false;
        m_Collider.enabled = true;
        SetColor(color * 3);
        ChangeAnim(Constant.ANIM_IDLE);
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
            if (!isAttacking && GameManager.Ins.IsState(GameState.Gameplay)) ChangeAnim(Constant.ANIM_IDLE);
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
        m_Transform.position = Vector3.Lerp(m_Transform.position, m_Transform.position + moveDir, moveSpeed * Time.deltaTime);
        m_Transform.rotation = Quaternion.Slerp(m_Transform.rotation, lookRotation, moveSpeed * Time.deltaTime);
    }

    public override void RemoveTarget(Character target)
    {
        if (this.target == target) UnMarkTarget();
        base.RemoveTarget(target);
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

    public override void OnHit(Character killer)
    {
        base.OnHit(killer);
        LevelManager.Ins.killer = killer.CharName;
    }

    public override void OnKill()
    {
        base.OnKill();
        cameraFollow.IncreaseOffset();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Ins.EndLevel(false);
    }

    public void Win()
    {
        isWin = true;
        ChangeAnim(Constant.ANIM_WIN);
    }
}
