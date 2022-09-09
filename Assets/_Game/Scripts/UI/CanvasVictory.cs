using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    public void HomeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.BackHome();
        Close();
    }

    public void NextLevelButton()
    {
        //UIManager.Ins.OpenUI(UIID.UICGamePlay);
        //GameManager.Ins.ChangeState(GameState.Gameplay);
        //Close();

        Debug.Log("Next Level");
    }
}
