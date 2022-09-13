using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Hammer, Knife, Candy2 }

public abstract class Weapon : GameUnit
{
    public float attackSpeed { get; private set; }
    public GameUnit bulletPrefab;

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
        SoundManager.Ins.PlaySound(SoundManager.Ins.throwWeapon);
        SimplePool.Spawn<Bullet>(bulletPrefab, position, rotation).OnInit(this);
    }

    public void HitTarget()
    {
        weaponHolder.HitTarget();
        scale = Vector3.Scale(scale, Constant.SCALE_VECTOR3);
        range *= Constant.SCALE_FLOAT;
    }
}
