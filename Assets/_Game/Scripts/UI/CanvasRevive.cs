using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRevive : UICanvas
{
    public Text timeText;
    public GameObject notEnough;

    private float timer;
    private int timeRemain;

    public void OnInit()
    {
        timeRemain = 5;
        timer = timeRemain;
        timeText.text = "5";
        notEnough.SetActive(false);
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
            SoundManager.Ins.PlaySound(SoundManager.Ins.countdown);
        }

        if (timer < -0.5f)
        {
            CloseButton();
        }
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<CanvasFail>(UIID.UICFail).OnInit();
        Close();
    }

    public void UseGoldButton()
    {
        if (LevelManager.Ins.OnRevive(true))
        {
            GameManager.Ins.ChangeState(GameState.Gameplay);
            UIManager.Ins.OpenUI(UIID.UICGamePlay);
            Close();
        }
        else
        {
            notEnough.SetActive(true);
        }
    }

    public void WatchAdButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        LevelManager.Ins.OnRevive(false);
        Close();
    }
}
