using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayGameButton()
    {
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
        LevelManager.Ins.Gameplay();
        Close();
    }

    public void ReadStringInput(string name)
    {
        LevelManager.Ins.SetPlayerName(name);
    }
}
