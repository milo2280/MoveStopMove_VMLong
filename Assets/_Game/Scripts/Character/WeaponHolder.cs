using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform myTransform;

    public Transform handTransform;
    public Transform bulletPoint;
    public Character character;

    private Weapon weapon;
    private float timer;
    private bool isAttack;
    private float respawnTime;

    private void Update()
    {
        WeaponOnHand();

        if (isAttack)
        {
            timer += Time.deltaTime;
            if (timer > respawnTime)
            {
                Respawn();
            }
        }
    }

    public void OnInit()
    {
        if (weapon == null)
        {
            weapon = Instantiate(DataManager.Ins.weapons[Random.Range(0, 3)], myTransform);
        }
        weapon.OnInit(this);
    }

    private void WeaponOnHand()
    {
        myTransform.position = handTransform.position;
        myTransform.rotation = handTransform.rotation;
    }

    public void Attack(float respawnTime)
    {
        weapon.Attack(bulletPoint.position, bulletPoint.rotation);
        isAttack = true;
        this.respawnTime = respawnTime;
        weapon.gameObject.SetActive(false);
    }

    private void Respawn()
    {
        isAttack = false;
        timer = 0f;
        weapon.gameObject.SetActive(true);
    }

    public void HitTarget()
    {
        character.HitTarget();
    }

    public float GetAttackRange()
    {
        return character.GetAttackRange();
    }
}
