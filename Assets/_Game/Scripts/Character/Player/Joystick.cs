using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform joystickTransform;
    public Transform stickTransform;

    private bool disabled;
    private float dragOffset, sqrDragOffset;
    private Vector3 rootPos, dragPos, dragDir;
    public Vector3 mouseDir { get; private set; }

    private void Start()
    {
        dragOffset = 150f;
        sqrDragOffset = dragOffset * dragOffset;
    }

    private void Update()
    {
        if (!disabled)
        {
            HandleMouseInput();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                rootPos = Input.mousePosition;
            }

            joystickTransform.gameObject.SetActive(true);
            joystickTransform.position = rootPos;

            dragPos = Input.mousePosition;
            dragDir = dragPos - rootPos;
            mouseDir = dragDir.normalized;

            if (dragDir.sqrMagnitude > sqrDragOffset)
            {
                dragPos = rootPos + mouseDir * dragOffset;
            }

            stickTransform.position = dragPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDir = Vector3.zero;
            joystickTransform.gameObject.SetActive(false);
        }
    }

    public void DisableJoystick()
    {
        disabled = true;
    }
}
