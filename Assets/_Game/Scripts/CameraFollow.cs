using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTranform, playerTransform;
    public Vector3 playingOffset, startOffset;
    public Quaternion playingAngle, startAngle;

    private Vector3 offset;
    private Vector3 newOffset;
    private bool isScaling, isPlaying;

    private void Start()
    {
        isPlaying = false;
        offset = startOffset;
        cameraTranform.rotation = startAngle;
    }

    private void LateUpdate()
    {
        GameStateTransition();
        FollowPlayer();
        Scaling();
    }

    private void FollowPlayer()
    {
        cameraTranform.position = playerTransform.position + offset;
    }

    private void GameStateTransition()
    {
        if (!isPlaying)
        {
            if (GameManager.Ins.IsState(GameState.Gameplay))
            {
                offset = Vector3.Lerp(offset, playingOffset, Time.deltaTime);
                cameraTranform.rotation = Quaternion.Slerp(cameraTranform.rotation, playingAngle, Time.deltaTime);
            }

            if (ChangeFinished(playingOffset, playingAngle) || GameManager.Ins.IsState(GameState.MainMenu))
            {
                isPlaying = true;
            }
        }
        else
        {
            if (GameManager.Ins.IsState(GameState.MainMenu))
            {
                offset = Vector3.Lerp(offset, startOffset, Time.deltaTime);
                cameraTranform.rotation = Quaternion.Slerp(cameraTranform.rotation, startAngle, Time.deltaTime);
            }

            if (ChangeFinished(startOffset, startAngle) || GameManager.Ins.IsState(GameState.Gameplay))
            {
                isPlaying = false;
            }
        }
    }

    private bool ChangeFinished(Vector3 position, Quaternion rotation)
    {
        bool changePositionFinished = (position - offset).sqrMagnitude < 0.001f;
        bool changeAngleFinished = Quaternion.Angle(cameraTranform.rotation, rotation) < 0.001f;

        return changeAngleFinished && changePositionFinished;
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
