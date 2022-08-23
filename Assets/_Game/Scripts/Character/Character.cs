using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float speed;
    public Transform charTransform;
    public Animator animator;

    public virtual void OnInit() { }
    public virtual void OnDespawn() { }
}
