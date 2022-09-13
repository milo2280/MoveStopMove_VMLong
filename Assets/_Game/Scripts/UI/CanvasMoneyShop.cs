using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMoneyShop : UICanvas
{
    public void CloseButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        LevelManager.Ins.ShowPlayer();
        Close();
    }

    public void RemoveAds()
    {
        Debug.Log("Remove Ads");
    }

    public void BuyGold()
    {
        Debug.Log("Buy gold");
    }

    public void BuyAllSkins()
    {
        Debug.Log("Buy all skins");
    }
}
