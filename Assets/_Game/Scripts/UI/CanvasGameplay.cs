using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    public void SettingButton()
    {
        UIManager.Ins.OpenUI(UIID.UICSetting);
        GameManager.Ins.ChangeState(GameState.Pause);
    }
}
