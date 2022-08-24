using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform enemyTransform;

    private bool isOn = false;
    private float playerRange;
    private float sqrplayerRange;

    private void Start()
    {
        playerRange = LevelManager.Ins.playerRange;
        sqrplayerRange = playerRange * playerRange;
    }

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
        return (enemyTransform.position - LevelManager.Ins.playerPos).sqrMagnitude > sqrplayerRange;
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
