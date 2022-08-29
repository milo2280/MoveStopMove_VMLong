using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float attackSpeed { get; private set; }
    public GameUnit bulletPrefab;
    public Transform weaponTransform;

    public Vector3 scale;
    public float range;

    private WeaponHolder weaponHolder;

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

    public void HitTarget()
    {
        weaponHolder.HitTarget();
        scale = Vector3.Scale(scale, Constant.SCALE_VECTOR3);
        range *= Constant.SCALE_FLOAT;
    }
}
