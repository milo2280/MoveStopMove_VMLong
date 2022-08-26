using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform handTransform;
    public Transform holderTransform;
    public Transform bulletPoint;
    public GameObject[] listWeaponPrefab;

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
        weaponObj = Instantiate(listWeaponPrefab[1], holderTransform);
        currentWeapon = weaponObj.GetComponent<Weapon>();
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
}
