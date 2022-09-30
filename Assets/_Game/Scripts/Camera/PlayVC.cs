using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayVC : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playVC;

    private CinemachineTransposer transposer;
    private Vector3 baseOffset, nextOffset;
    private bool isIncreasing;

    private void Awake()
    {
        transposer = playVC.GetCinemachineComponent<CinemachineTransposer>();
        baseOffset = transposer.m_FollowOffset;
        nextOffset = baseOffset;
    }

    private void LateUpdate()
    {
        if (isIncreasing)
        {
            Increasing();
        }
    }

    public void IncreaseOffset()
    {
        isIncreasing = true;
        nextOffset += nextOffset * Constant.TEN_PERCENT;
    }

    private void Increasing()
    {
        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, nextOffset, Time.deltaTime);
        if ((nextOffset - transposer.m_FollowOffset).sqrMagnitude < 0.001f) isIncreasing = false;
    }

    public void OnReset()
    {
        transposer.m_FollowOffset = baseOffset;
        nextOffset = baseOffset;
    }
}
