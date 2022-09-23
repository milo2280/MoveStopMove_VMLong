using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    public Transform joystickTransform;
    public Transform stickTransform;

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
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (!IsMouseOverUI())
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
            else
            {
                mouseDir = Vector3.zero;
                joystickTransform.gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDir = Vector3.zero;
            joystickTransform.gameObject.SetActive(false);
        }
    }

    private bool IsMouseOverUI()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            for (int i = 0; i < raycastResults.Count; i++)
            {
                if (raycastResults[i].gameObject.CompareTag(Constant.TAG_BUTTON))
                {
                    return true;
                }
            }

            return false;
        }
        
        return true;
    }
}
