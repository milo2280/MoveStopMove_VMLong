using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float speed;
    public Transform charTransform;
    public Animator animator;
    public Weapon weapon;

    protected string currentAnim;

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

    public void Attack()
    {
        weapon.Attack();
    }

    public void ChangeAnim(string nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            currentAnim = nextAnim;
            animator.SetTrigger(nextAnim);
        }
    }
}
