using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : GameUnit
{
    public float attackSpeed { get; private set; }
    public GameUnit bulletPrefab;
    public Transform weaponTransform;
    public Collider rangeCollider;

    public Vector3 scale;
    public float range;
    public float bulletSpeed;

    protected WeaponHolder weaponHolder;

    public void OnInit(WeaponHolder weaponHolder)
    {
        this.weaponHolder = weaponHolder;
        scale = new Vector3(1f, 1f, 1f);
        range = weaponHolder.GetAttackRange();
    }

    public virtual void Attack(Vector3 position, Quaternion rotation) 
    {
        SimplePool.Spawn<Bullet>(bulletPrefab, position, rotation).OnInit(this);
    }

    public void HitTarget(Collider target)
    {
        weaponHolder.HitTarget(target);
        scale = Vector3.Scale(scale, Constant.SCALE_VECTOR3);
        range *= Constant.SCALE_FLOAT;
    }
}
