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
        LevelManager.Ins.MainMenu();
        Close();
    }

    public void RestartLevelButton()
    {
        //UIManager.Ins.OpenUI(UIID.UICGamePlay);
        //GameManager.Ins.ChangeState(GameState.Gameplay);
        //Close();

        Debug.Log("Restart Level");
    }
}
