using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTranform, playerTransform;
    public Vector3 offset;
    public Quaternion playingAngle;
    public Vector3 startOffset;
    public Quaternion startAngle;

    private Vector3 newOffset;
    private bool isScaling;

    private void Start()
    {
        cameraTranform.position = playerTransform.position + startOffset;
        cameraTranform.rotation = startAngle;
    }

    private void LateUpdate()
    {
        //FollowPlayer();
        //Scaling();
    }

    private void FollowPlayer()
    {
        cameraTranform.position = playerTransform.position + offset;
    }

    public void ScaleOffset()
    {
        isScaling = true;
        newOffset = Vector3.Scale(offset, Constant.SCALE_VECTOR3);
    }

    private void Scaling()
    {
        if (isScaling)
        {
            offset = Vector3.Lerp(offset, newOffset, Time.deltaTime);
            if ((newOffset - offset).sqrMagnitude < 0.01f)
            {
                isScaling = false;
            }
        }
    }
}
