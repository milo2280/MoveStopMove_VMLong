using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform handTransform;
    public Transform holderTransform;
    public Transform bulletPoint;
    public Character character;

    private Weapon currentWeapon;

    private void Start()
    {
        WeaponOnHand();
        GenerateWeapon();
    }

    private void Update()
    {
        WeaponOnHand();
    }

    private void GenerateWeapon()
    {
        currentWeapon = SimplePool.SpawnOne<Weapon>(DataManager.Ins.weapons[Random.Range(0, 3)], Vector3.zero, holderTransform);
        currentWeapon.OnInit(this);
    }

    private void WeaponOnHand()
    {
        holderTransform.position = handTransform.position;
        holderTransform.rotation = handTransform.rotation;
    }

    public void Attack()
    {
        currentWeapon.Attack(bulletPoint.position, bulletPoint.rotation);
    }

    public void HitTarget()
    {
        character.KillAnEnemy();
    }

    public float GetAttackRange()
    {
        return character.GetAttackRange();
    }
}
