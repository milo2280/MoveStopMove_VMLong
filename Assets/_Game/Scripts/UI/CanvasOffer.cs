using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOffer : UICanvas
{
    public GameObject LoseItButton;

    private float timer;
    private bool isLoseItActive;

    private void OnEnable()
    {
        timer = 0;
        isLoseItActive = false;
        LoseItButton.SetActive(false);
        LevelManager.Ins.HidePlayer();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > Constant.DELAY_BUTTON && !isLoseItActive)
        {
            isLoseItActive = true;
            LoseItButton.SetActive(true);
        }
    }

    private void OnDisable()
    {
        LevelManager.Ins.ShowPlayer();
    }

    public void CloseButton()
    {
        Close();
    }
}
