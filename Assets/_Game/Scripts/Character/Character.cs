using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : GameUnit, IHit
{
    public Animator animator;
    public Collider myCollider;
    public Transform myTransform;

    public NameBar nameBar;
    public WeaponHolder weaponHolder;

    public float speed;
    public float attackRange;
    public float attackSpeed;

    protected float currentAttackRange;
    protected int score;
    protected Vector3 nextScale;
    protected Character target;
    protected Collider targetCollider;
    protected List<Collider> listTargetCollider = new List<Collider>();

    protected string currentAnim;
    protected bool isAttacking, isThrew, isDelaying, isDead;

    protected float attackTimer, delayTimer;

    protected const float ATTACK_DURARION = 1f;
    protected const float THROW_DURATION = 2f / 5f;
    protected const float HAND_BACK_DURATION = 3f / 5f;
    protected const float DELAY_DURATION = 1f;

    public virtual void OnInit() 
    {
        isDead = false;
        myCollider.enabled = true;
        animator.SetFloat(Constant.ANIM_ATTACK_SPEED, attackSpeed);
        ChangeScore(0);
        ResetSize();
        ResetTarget();
        weaponHolder.OnInit();
    }

    public void OnHit()
    {
        OnDeath();
    }

    public virtual void OnDeath()
    {
        isDead = true;
        myCollider.enabled = false;
        ChangeAnim(Constant.ANIM_DEAD);
        if (isAttacking) StopAttack();
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

    public void AddTargetCollider(Collider targetCollider)
    {
        listTargetCollider.Add(targetCollider);
    }

    public virtual void RemoveTargetCollider(Collider targetCollider)
    {
        listTargetCollider.Remove(targetCollider);
    }

    public bool ScanTarget()
    {
        for (int i = 0; i < listTargetCollider.Count; i++)
        {
            if (listTargetCollider[i].enabled == false)
            {
                RemoveTargetCollider(listTargetCollider[i]);
            }
        }

        if (listTargetCollider.Count > 0)
        {
            if (!listTargetCollider.Contains(targetCollider))
            {
                targetCollider = listTargetCollider[0];
                target = Cache<Character>.Get(targetCollider);
            }

            return true;
        }
        else
        {
            target = null;
            targetCollider = null;

            return false;
        }
    }

    public void Attack()
    {
        if (!isAttacking && !isDelaying)
        {
            isAttacking = true;
            myTransform.LookAt(target.myTransform);
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

        if (attackTimer > THROW_DURATION / attackSpeed && !isThrew)
        {
            weaponHolder.Attack(HAND_BACK_DURATION / attackSpeed);
            isDelaying = true;
            isThrew = true;
        }

        if (attackTimer > ATTACK_DURARION / attackSpeed)
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
        if (delayTimer > DELAY_DURATION / attackSpeed)
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

    public void HitTarget()
    {
        OnKill();
    }

    public virtual void OnKill()
    {
        IncreaseSize();
        ChangeScore(++score);
    }

    private void IncreaseSize()
    {
        myTransform.localScale = nextScale;
        currentAttackRange *= Constant.SCALE_FLOAT;
        nextScale = Vector3.Scale(nextScale, Constant.SCALE_VECTOR3);
    }

    private void ResetSize()
    {
        myTransform.localScale = new Vector3(1f, 1f, 1f);
        currentAttackRange = attackRange;
        nextScale = Constant.SCALE_VECTOR3;
    }

    private void ResetTarget()
    {
        target = null;
        targetCollider = null;
        listTargetCollider.Clear();
    }

    public virtual void ChangeScore(int score)
    {
        this.score = score;
        nameBar.ChangeScore(score);
    }

    public void SetName(string name)
    {
        nameBar.SetName(name);
    }

    public float GetAttackRange()
    {
        return currentAttackRange;
    }
}
