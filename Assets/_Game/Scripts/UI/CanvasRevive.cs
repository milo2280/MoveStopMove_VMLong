using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRevive : UICanvas
{
    public Text timeText;

    private float timer;
    private float timeRemain;

    public void OnInit()
    {
        timeRemain = 5;
        timer = timeRemain;
        timeText.text = "5";
    }

    private void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        timer -= Time.deltaTime;

        if (timer < timeRemain - 1)
        {
            timeRemain--;
            timeText.text = timeRemain.ToString();
        }

        if (timeRemain < Constant.ZERO && timer < -0.5f)
        {
            CloseButton();
        }
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI(UIID.UICFail);
        Close();
    }

    public void UseGoldButton()
    {
        Debug.Log("Use Gold");
    }

    public void WatchAdButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        LevelManager.Ins.OnRevive();
        Close();
    }
}
