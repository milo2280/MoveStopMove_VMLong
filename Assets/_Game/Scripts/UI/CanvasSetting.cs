using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
        Close();
    }

    public void HomeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.BackHome();
        Close();
    }
}
