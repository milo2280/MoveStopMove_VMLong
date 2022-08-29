using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform handTransform;
    public Transform holderTransform;
    public Transform bulletPoint;
    public GameObject[] listWeaponPrefab;
    public Character character;

    private GameObject weaponObj;
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
        weaponObj = Instantiate(listWeaponPrefab[Random.Range(0, 3)], holderTransform);
        currentWeapon = weaponObj.GetComponent<Weapon>();
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
