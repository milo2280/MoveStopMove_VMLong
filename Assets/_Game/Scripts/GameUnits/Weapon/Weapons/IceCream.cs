using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : Weapon
{
    private Quaternion leftRot, rightRot;

    private const float ANGLE_DEVIATION = 20f;

    private void Awake()
    {
        bulletSpeed = 10f;
    }

    public override void Attack()
    {
        base.Attack();

        leftRot = bulletPoint.rotation * Quaternion.Euler(0f, -ANGLE_DEVIATION, 0f);
        rightRot = bulletPoint.rotation * Quaternion.Euler(0f, ANGLE_DEVIATION, 0f);

        SimplePool.Spawn<Bullet>(bulletPrefab, bulletPoint.position, leftRot).OnInit(character);
        SimplePool.Spawn<Bullet>(bulletPrefab, bulletPoint.position, rightRot).OnInit(character);
    }
}
