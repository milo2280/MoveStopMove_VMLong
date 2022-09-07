using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTransform, playerTransform;
    public Vector3 playOffset, menuOffset;
    public Quaternion playAngle, menuAngle;

    private Vector3 offset, nextOffset;
    private Quaternion nextAngle;
    private bool offsetChanged, angleChanged;

    private void Start()
    {
        offset = menuOffset;
        nextAngle = menuAngle;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (offsetChanged)
        {
            offset = Vector3.Lerp(offset, nextOffset, Time.deltaTime);
            if ((nextOffset - offset).sqrMagnitude < 0.001f) offsetChanged = false;
        }

        cameraTransform.position = playerTransform.position + offset;

        if (angleChanged)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, nextAngle, Time.deltaTime);
            if (Quaternion.Angle(cameraTransform.rotation, nextAngle) < 0.01f) angleChanged = false;
        }
    }

    public void GameplayPos()
    {
        nextOffset = playOffset;
        nextAngle = playAngle;
        offsetChanged = angleChanged = true;
    }

    public void MainMenuPos()
    {
        nextOffset = menuOffset;
        nextAngle = menuAngle;
        offsetChanged = angleChanged = true;
    }

    public void ScaleOffset()
    {
        nextOffset = Vector3.Scale(nextOffset, Constant.SCALE_VECTOR3);
        offsetChanged = true;
    }
}
