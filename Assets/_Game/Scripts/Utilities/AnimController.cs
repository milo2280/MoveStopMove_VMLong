using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerAnim
{
    idle,
    run,
    attack,
    dead,
    win
}

public enum CinemachineAnim
{
    menu,
    play,
    skin
}

public class AnimController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Enum currentAnim;

    public void ChangeAnim(Enum nextAnim)
    {
        string current, next;

        if (currentAnim != null)
        {
            current = Enum2String(currentAnim);
            animator.ResetTrigger(current);
        }

        if (currentAnim != nextAnim)
        {
            next = Enum2String(nextAnim);
            animator.SetTrigger(next);
            currentAnim = nextAnim;
        }
    }

    private string Enum2String(Enum e)
    {
        return e.ToString();
    }
}
