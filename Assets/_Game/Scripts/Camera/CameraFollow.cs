using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayVC playVC;
    [SerializeField]
    private AnimController CMState;

    public void MenuVC()
    {
        CMState.ChangeAnim(CinemachineAnim.menu);
        playVC.OnReset();
    }

    public void PlayVC()
    {
        CMState.ChangeAnim(CinemachineAnim.play);
    }

    public void SkinVC()
    {
        CMState.ChangeAnim(CinemachineAnim.skin);
    }

    public void IncreaseOffset()
    {
        playVC.IncreaseOffset();
    }
}
