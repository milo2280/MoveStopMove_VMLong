using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joystick;

    private Vector3 mouseDir, moveDir;
    private Quaternion lookRotation;
    private bool isStop = true;

    private void Start()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }

    private void Update()
    {
        JoystickMove();
        DetectEnemy();
    }

    private void JoystickMove()
    {
        mouseDir = joystick.mouseDir;

        if ((mouseDir - Vector3.zero).sqrMagnitude > 0.01f)
        {
            CalculateMoveDir();
            Move();
            ChangeAnim(Constant.ANIM_RUN);
            isStop = false;
        }
        else
        {
            ChangeAnim(Constant.ANIM_IDLE);
            isStop = true;
        }
    }

    private void CalculateMoveDir()
    {
        moveDir.x = mouseDir.x;
        moveDir.z = mouseDir.y;
        lookRotation = Quaternion.LookRotation(moveDir);
    }

    private void Move()
    {
        charTransform.position = Vector3.Lerp(charTransform.position, charTransform.position + moveDir, speed * Time.deltaTime);
        charTransform.rotation = Quaternion.Slerp(charTransform.rotation, lookRotation, speed * Time.deltaTime);
    }

    private void DetectEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(charTransform.position, attackRange);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag(Constant.TAG_ENEMY))
            {
                Enemy enemy = Cache<Enemy>.Get(colliders[i]);
                enemy.Targeted();
                break;
            }
        }
    }
}
