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
    protected bool isAttacking, isDelaying, isDead;
    protected Coroutine lastAttack, lastWeaponAttack, lastFinish;

    protected const float DELAY_ATTACK = 3f;
    protected const float ATTACK_ANIM_DURATION = 1f;

    public virtual void OnInit() 
    {
        isDead = false;
        myCollider.enabled = true;
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

    public void AddTargetCollider(Collider target)
    {
        listTargetCollider.Add(target);
    }

    public virtual void RemoveTargetCollider(Collider target)
    {
        listTargetCollider.Remove(target);
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
            lastAttack = StartCoroutine(IEAttack());
        }
    }

    protected IEnumerator IEAttack()
    {
        yield return new WaitForSeconds(1 / attackSpeed);

        myTransform.LookAt(target.myTransform);
        ChangeAnim(Constant.ANIM_ATTACK);
        lastWeaponAttack = StartCoroutine(IEWeaponAttack());
    }

    protected IEnumerator IEWeaponAttack()
    {
        yield return new WaitForSeconds(ATTACK_ANIM_DURATION / 5 * 2);

        isDelaying = true;
        weaponHolder.Attack();
        lastFinish = StartCoroutine(IEFinishAttack());
        StartCoroutine(IEDelayAfterAttack());
    }

    protected IEnumerator IEFinishAttack()
    {
        yield return new WaitForSeconds(ATTACK_ANIM_DURATION / 5 * 3);

        isAttacking = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    protected IEnumerator IEDelayAfterAttack()
    {
        yield return new WaitForSeconds(DELAY_ATTACK / attackSpeed);

        isDelaying = false;
    }

    public void StopAttack()
    {
        isAttacking = false;
        if (lastAttack != null) StopCoroutine(lastAttack);
        if (lastWeaponAttack != null) StopCoroutine(lastWeaponAttack);
        if (lastFinish != null) StopCoroutine(lastFinish);
    }

    public void HitTarget(Collider target)
    {
        OnKill(target);
    }

    public virtual void OnKill(Collider target)
    {
        RemoveTargetCollider(target);
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

    public virtual void ChangeScore(int point)
    {
        this.score = point;
        nameBar.ChangeScore(point);
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
