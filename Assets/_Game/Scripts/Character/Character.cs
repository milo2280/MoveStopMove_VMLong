using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : GameUnit, IHit
{
    public float speed;
    public float attackRange;
    public float attackSpeed;
    public Transform charTransform;
    public Animator animator;
    public WeaponHolder weaponHolder;
    public Collider charCollider;

    protected Character target;

    protected Collider targetCollider;
    protected List<Collider> listTargetCollider = new List<Collider>();

    protected bool isAttacking, isDelaying, isDead;
    protected string currentAnim;
    protected Coroutine lastAttack, lastWeaponAttack, lastFinish;
    protected Vector3 nextScale;

    protected const float DELAY_ATTACK = 1f;
    protected const float MAX_ATTACK_SPEED = 1f;
    protected const float ATTACK_ANIM_DURATION = 1f;

    public virtual void OnInit() 
    {
        nextScale = Constant.SCALE_VECTOR3;
    }

    public void OnHit()
    {
        OnDeath();
    }

    public virtual void OnDeath()
    {
        isDead = true;
        charCollider.enabled = false;
        ChangeAnim(Constant.ANIM_DEAD);
        if (isAttacking) StopAttack();
    }

    public virtual void OnDespawn() { }

    public void ChangeAnim(string nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            animator.ResetTrigger(currentAnim);
            animator.SetTrigger(nextAnim);
            currentAnim = nextAnim;
        }
    }

    public void AddTargetCollider(Collider other)
    {
        listTargetCollider.Add(other);
    }

    public virtual void RemoveTargetCollider(Collider other)
    {
        listTargetCollider.Remove(other);
    }

    public bool ScanTarget()
    {
        for (int i = 0; i < listTargetCollider.Count; i++)
        {
            if (listTargetCollider[i].enabled == false) RemoveTargetCollider(listTargetCollider[i]);
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
            targetCollider = null;
            target = null;

            return false;
        }
    }

    public void Attack()
    {
        if (!isAttacking && !isDelaying)
        {
            isAttacking = true;
            lastAttack = StartCoroutine(IEAttack());
        }
    }

    protected IEnumerator IEAttack()
    {
        yield return new WaitForSeconds(MAX_ATTACK_SPEED - attackSpeed);

        charTransform.LookAt(target.charTransform);
        ChangeAnim(Constant.ANIM_ATTACK);
        lastWeaponAttack = StartCoroutine(IEWeaponAttack());
    }

    protected IEnumerator IEWeaponAttack()
    {
        yield return new WaitForSeconds(ATTACK_ANIM_DURATION / 2);

        isDelaying = true;
        weaponHolder.Attack();
        lastFinish = StartCoroutine(IEFinishAttack());
        StartCoroutine(IEDelayAfterAttack());
    }

    protected IEnumerator IEFinishAttack()
    {
        yield return new WaitForSeconds(ATTACK_ANIM_DURATION / 2);

        isAttacking = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    protected IEnumerator IEDelayAfterAttack()
    {
        yield return new WaitForSeconds(DELAY_ATTACK);

        isDelaying = false;
    }

    public void StopAttack()
    {
        isAttacking = false;
        if (lastAttack != null) StopCoroutine(lastAttack);
        if (lastWeaponAttack != null) StopCoroutine(lastWeaponAttack);
        if (lastFinish != null) StopCoroutine(lastFinish);
    }

    public virtual void HitTarget(Collider target)
    {
        RemoveTargetCollider(target);
        charTransform.localScale = nextScale;
        attackRange *= Constant.SCALE_FLOAT;
        nextScale = Vector3.Scale(nextScale, Constant.SCALE_VECTOR3);
    }

    public float GetAttackRange()
    {
        return attackRange;
    }
}
