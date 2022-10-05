using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform m_Transform;

    public Camera mainCamera;

    public Material[] materials;

    private Vector3 viewPos;

    private void Start()
    {
        meshRenderer.material = materials[0];
    }

    private void Update()
    {
        HideAndShowObstacle();
    }

    private void HideAndShowObstacle()
    {
        viewPos = mainCamera.WorldToViewportPoint(m_Transform.position);

        if (viewPos.z < 0 || (viewPos.z > 0 && viewPos.y < 0.7f))
        {
            meshRenderer.material = materials[1];
        }
        else
        {
            meshRenderer.material = materials[0];
        }
    }
}
