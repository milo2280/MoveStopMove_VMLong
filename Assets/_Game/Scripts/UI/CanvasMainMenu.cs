using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void ReadStringInput(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            name = Constant.DEFAULT_NAME;
        }

        LevelManager.Ins.SetPlayerName(name);
    }

    public void PlayGameButton()
    {
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
        LevelManager.Ins.PlayGame();
        Close();
    }

    public void WeaponButton()
    {
        Debug.Log("Weapon");
    }

    public void SkinButton()
    {
        Debug.Log("Skin");
    }
}
