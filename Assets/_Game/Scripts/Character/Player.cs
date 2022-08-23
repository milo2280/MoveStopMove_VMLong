using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Joystick joystick;

    private Vector3 mouseDir, moveDir;
    private Quaternion lookRotation;

    private void Update()
    {
        JoystickMove();
    }

    private void JoystickMove()
    {
        mouseDir = joystick.mouseDir;

        if ((mouseDir - Vector3.zero).sqrMagnitude > 0.01f)
        {
            CalculateMoveDir();
            Move();
            animator.SetTrigger(Constant.ANIM_RUN);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.ResetTrigger(Constant.ANIM_RUN);
            animator.SetTrigger(Constant.ANIM_IDLE);
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
}