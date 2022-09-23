using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : GameUnit
{
    public WeaponClass weaponClass;
    public WeaponType weaponType;
    public Buff buff;

    public GameUnit bulletPrefab;

    public float bulletSpeed;

    public Character character;
    protected Transform bulletPoint;

    public void OnInit(Character character)
    {
        this.character = character;
        this.bulletPoint = character.bulletPoint;
    }

    public virtual void Attack() 
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins.throwWeapon);
        SimplePool.Spawn<Bullet>(bulletPrefab, bulletPoint.position, bulletPoint.rotation).OnInit(character);
    }
}
