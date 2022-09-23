using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform m_Transform;

    public Transform handTransform;
    public Character character;

    private bool isAttack;
    private float timer, respawnTime;

    private Weapon weapon;
    private Dictionary<WeaponType, Weapon> dictUsedWeapon = new Dictionary<WeaponType, Weapon>();

    private void Update()
    {
        UpdatePosition();

        if (isAttack)
        {
            timer += Time.deltaTime;
            if (timer > respawnTime)
            {
                RespawnWeapon();
            }
        }
    }

    public Weapon OnInit(WeaponType type)
    {
        if (weapon != null)
        {
            if (weapon.weaponType == type) return weapon;
            weapon.gameObject.SetActive(false);
        }

        if (dictUsedWeapon.ContainsKey(type))
        {
            weapon = dictUsedWeapon[type];
            weapon.gameObject.SetActive(true);
        }
        else
        {
            weapon = Instantiate(WeaponManager.Ins.dictWeapon[type], m_Transform);
            dictUsedWeapon.Add(weapon.weaponType, weapon);
            weapon.OnInit(character);
        }
        
        return weapon;
    }

    private void UpdatePosition()
    {
        m_Transform.position = handTransform.position;
        m_Transform.rotation = handTransform.rotation;
    }

    public void ThrowWeapon(float respawnTime)
    {
        isAttack = true;
        this.respawnTime = respawnTime;
        weapon.gameObject.SetActive(false);
    }

    private void RespawnWeapon()
    {
        timer = 0f;
        isAttack = false;
        weapon.gameObject.SetActive(true);
    }
}
