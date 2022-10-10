using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform enemyTransform;

    public void EnableMark()
    {
        meshRenderer.enabled = true;
    }

    public void DisableMark()
    {
        meshRenderer.enabled = false;
    }
}
