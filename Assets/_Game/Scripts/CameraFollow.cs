using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTranform, playerTransform;
    public Vector3 offset;

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        cameraTranform.position = playerTransform.position + offset;
    }

    public void IncreaseOffset()
    {
        offset = Vector3.Scale(offset, Constant.SCALE_VECTOR3);
    }
}
