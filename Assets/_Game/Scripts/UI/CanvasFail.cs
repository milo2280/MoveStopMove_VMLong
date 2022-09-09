using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    public void HomeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.BackHome();
        Close();
    }

    public void RestartLevelButton()
    {
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
        LevelManager.Ins.RestartLevel();
        Close();
    }
}
