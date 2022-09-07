using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    public void SettingButton()
    {
        UIManager.Ins.OpenUI(UIID.UICSetting);
        //GameManager.Ins.ChangeState(GameState.Pause);
        Close();
    }

    public void Victory()
    {
        UIManager.Ins.OpenUI(UIID.UICVictory);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        Close();
    }

    public void Fail()
    {
        UIManager.Ins.OpenUI(UIID.UICFail);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        Close();
    }
}
