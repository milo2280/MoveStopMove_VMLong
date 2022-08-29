using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy2 : Weapon
{
    private Quaternion leftRot, rightRot;

    private const float ANGLE_DEVIATION = 20f;

    public override void Attack(Vector3 position, Quaternion rotation)
    {
        base.Attack(position, rotation);

        leftRot = rotation * Quaternion.Euler(0f, -ANGLE_DEVIATION, 0f);
        rightRot = rotation * Quaternion.Euler(0f, ANGLE_DEVIATION, 0f);

        base.Attack(position, leftRot);
        base.Attack(position, rightRot);
    }
}
