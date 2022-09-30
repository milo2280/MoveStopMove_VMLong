using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasShopSkin : UICanvas
{
    public Player player;
    public Text goldText;
    public Button[] optionButtons;
    public SkinOption[] skinOptions;
    private SkinClass currentClass;
    private Skin[] currentSkin;

    private void OnEnable()
    {
        OnInit();
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int option = i;
            optionButtons[i].onClick.AddListener(delegate { ChooseSkin(option); });
        }
    }

    public void OnInit()
    {
        player = LevelManager.Ins.player;
        LevelManager.Ins.SkinShop();
        UpdateGold();

        if (PlayerData.Ins.equippedSkin.Count == 0)
        {
            currentClass = SkinClass.Hair;
            currentSkin = SkinManager.Ins.dictSkinClass[currentClass];
        }
        else
        {
            //currentClass
        }

        ChooseSkin(0);
        ShowOption(currentClass);
    }

    private void ChooseSkin(int option)
    {
        player.WearSkin(currentSkin[option].skinType);
    }

    private void UpdateGold()
    {
        goldText.text = PlayerData.Ins.gold.ToString();
    }

    private void ShowOption(SkinClass skinClass)
    {
        Skin[] skins = SkinManager.Ins.dictSkinClass[skinClass];

        for (int i = 0; i < skins.Length; i++)
        {
            skinOptions[i].ShowSkin(skins[i]);
        }
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        LevelManager.Ins.BackHome();
        Close();
    }

    public void HairButton()
    {

    }

    public void PantButton()
    {

    }
}
