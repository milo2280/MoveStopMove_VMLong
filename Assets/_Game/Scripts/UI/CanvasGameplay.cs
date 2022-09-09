using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : UICanvas
{
    public Text enemyRemain;

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

    public void UpdateEnemyRemain(int remain)
    {
        enemyRemain.text = "Alive: " + remain.ToString();
    }
}
