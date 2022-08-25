using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public float attackSpeed;
    public Transform charTransform;
    public Animator animator;
    public Weapon weapon;
    public Collider charCollider;

    protected bool isAttacking = false;
    protected string currentAnim;
    protected Collider targetCollider;
    protected Coroutine lastPrepare, lastAttack;

    protected const float MAX_ATTACK_SPEED = 5f;
    protected const float ATTACK_ANIM_DURATION = 1f;

    public virtual void OnInit() { }
    public virtual void OnDespawn() { }

    public void OnHit()
    {
        OnDeath();
    }

    public void OnDeath()
    {
        OnDespawn();
    }

    public void ChangeAnim(string nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            if (nextAnim != Constant.ANIM_ATTACK) currentAnim = nextAnim;
            animator.SetTrigger(nextAnim);
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            lastPrepare = StartCoroutine(PrepareToAttack());
        }
    }

    protected IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(MAX_ATTACK_SPEED - attackSpeed);

        LookAtTarget();
        ChangeAnim(Constant.ANIM_ATTACK);
        lastAttack = StartCoroutine(StartAttacking());
    }

    protected IEnumerator StartAttacking()
    {
        yield return new WaitForSeconds(ATTACK_ANIM_DURATION / 2);

        isAttacking = false;
    }

    protected void LookAtTarget()
    {
        Character other = Cache<Character>.Get(targetCollider);
        charTransform.LookAt(other.charTransform);
    }

    public void StopAttack()
    {
        isAttacking = false;
        if (lastPrepare != null) StopCoroutine(lastPrepare);
        if (lastAttack != null) StopCoroutine(lastAttack);
    }

    public bool DetectEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(charTransform.position, attackRange);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (charCollider != colliders[i] && colliders[i].CompareTag(Constant.TAG_CHARACTER))
            {
                targetCollider = colliders[i];
                return true;
            }
        }

        return false;
    }
}
