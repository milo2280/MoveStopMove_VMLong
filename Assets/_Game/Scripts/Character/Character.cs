using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : GameUnit, IHit
{
    public Animator animator;
    public Collider m_Collider;
    public Transform m_Transform;

    public Hand hand;
    public HeadBar headBar;
    public Transform bulletPoint;
    public SkinnedMeshRenderer bodyMesh, pantMesh;

    public int score;
    public bool isDead;
    public float baseRange, baseAS, baseMS;
    public float range, attackSpeed, moveSpeed;

    protected Color color;
    protected Vector3 nextScale;
    protected Weapon weapon;
    protected Character target;
    protected List<Character> targets = new List<Character>();

    protected string currentAnim;
    protected bool isAttacking, isThrew, isDelaying;

    protected float attackTimer, delayTimer;

    protected const float ATTACK_DURARION = 1f;
    protected const float THROW_DURATION = 2f / 5f;
    protected const float HAND_BACK_DURATION = 3f / 5f;
    protected const float DELAY_DURATION = 1f;

    public virtual void OnInit() 
    {
        moveSpeed = baseMS;
        attackSpeed = baseAS;
        range = baseRange;

        isDead = false;
        m_Collider.enabled = true;
        SetColor();
        animator.SetFloat(Constant.ANIM_ATTACK_SPEED, GetAttackSpeed());
        UpdateScore(0);
        InitSize();
        InitTarget();
    }

    public virtual void SetColor()
    {
        bodyMesh.material.color = color;
        pantMesh.material.color = color;
        headBar.SetColor(color);
    }

    public void OnHit()
    {
        OnDeath();
    }

    public virtual void OnDeath()
    {
        isDead = true;
        m_Collider.enabled = false;
        TurnGray();
        ChangeAnim(Constant.ANIM_DEAD);
        if (isAttacking) StopAttack();
        SoundManager.Ins.PlaySound(SoundManager.Ins.die);
    }

    public void TurnGray()
    {
        bodyMesh.material.color = color / 3;
        pantMesh.material.color = color / 3;
        headBar.SetColor(color / 3);
    }

    public virtual void OnDespawn() { }

    public void ChangeAnim(string nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            if (!string.IsNullOrEmpty(currentAnim))
            {
                animator.ResetTrigger(currentAnim);
            }
            animator.SetTrigger(nextAnim);
            currentAnim = nextAnim;
        }
    }

    private void InitTarget()
    {
        target = null;
        targets.Clear();
    }

    public void AddTarget(Character target)
    {
        targets.Add(target);
    }

    public virtual void RemoveTarget(Character target)
    {
        targets.Remove(target);
    }

    public bool ScanTarget()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].isDead) RemoveTarget(targets[i]);
        }

        if (targets.Count > 0)
        {
            if (!targets.Contains(target)) target = targets[0];
            return true;
        }
        else
        {
            target = null;
            return false;
        }
    }

    public void Attack()
    {
        if (!isAttacking && !isDelaying)
        {
            isAttacking = true;
            m_Transform.LookAt(target.m_Transform);
            ChangeAnim(Constant.ANIM_ATTACK);
        }
    }

    public void Attacking()
    {
        attackTimer += Time.deltaTime;

        if (!ScanTarget() && !isThrew)
        {
            ChangeAnim(Constant.ANIM_IDLE);
            StopAttack();
        }

        if (attackTimer > THROW_DURATION / GetAttackSpeed() && !isThrew)
        {
            weapon.Attack();
            hand.ThrowWeapon(HAND_BACK_DURATION / GetAttackSpeed());
            isDelaying = true;
            isThrew = true;
        }

        if (attackTimer > ATTACK_DURARION / GetAttackSpeed())
        {
            attackTimer = 0;
            isAttacking = false;
            isThrew = false;
            ChangeAnim(Constant.ANIM_IDLE);
        }
    }

    public void Delaying()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer > DELAY_DURATION / GetAttackSpeed())
        {
            delayTimer = 0;
            isDelaying = false;
        }
    }

    public void StopAttack()
    {
        isAttacking = false;
        isThrew = false;
        attackTimer = 0;
    }

    public virtual void OnKill()
    {
        IncreaseSize();
        UpdateScore(++score);
    }

    private void IncreaseSize()
    {
        m_Transform.localScale = nextScale;
        range *= Constant.SCALE_FLOAT;
        nextScale = Vector3.Scale(nextScale, Constant.SCALE_VECTOR3);
    }

    private void InitSize()
    {
        m_Transform.localScale = new Vector3(1f, 1f, 1f);
        range = baseRange;
        nextScale = Constant.SCALE_VECTOR3;
    }

    public virtual void UpdateScore(int score)
    {
        this.score = score;
        headBar.ChangeScore(score);
    }

    public virtual void SetName(string name)
    {
        headBar.SetName(name);
    }

    public float GetAttackRange()
    {
        return range;
    }

    private float GetAttackSpeed()
    {
        return attackSpeed / 100;
    }

    //public virtual void  

    public void AddBuff()
    {
        if (weapon != null)
        {
            AddWeaponBuff();
        }
    }

    public void AddWeaponBuff()
    {

    }
}
