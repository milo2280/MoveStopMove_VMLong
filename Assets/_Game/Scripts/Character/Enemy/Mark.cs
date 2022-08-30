using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform enemyTransform;

    private bool isOn = false;

    private void Update()
    {
        if (isOn)
        {
            if (OutOfPlayerRange())
            {
                DisableMark();
            }
        }
    }

    private bool OutOfPlayerRange()
    {
        float distance = (enemyTransform.position - LevelManager.Ins.playerPos).sqrMagnitude;
        float range = LevelManager.Ins.playerRange * LevelManager.Ins.playerRange;
        return distance > range;
    }

    public void EnableMark()
    {
        if (!isOn)
        {
            meshRenderer.enabled = true;
            isOn = true;
        }
    }

    private void DisableMark()
    {
        meshRenderer.enabled = false;
        isOn = false;
    }
}
