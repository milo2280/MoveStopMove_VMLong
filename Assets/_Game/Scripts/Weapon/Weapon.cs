using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float attackSpeed { get; private set; }

    public virtual void Attack() { }
}
