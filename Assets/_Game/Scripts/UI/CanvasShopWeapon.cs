using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShopWeapon : UICanvas
{
    public void CloseButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        LevelManager.Ins.ShowPlayer();
        Close();
    }
}
