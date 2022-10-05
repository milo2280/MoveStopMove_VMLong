using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasShopSkin : UICanvas
{
    public Player player;
    public Text goldText;
    public Text buffText;
    public Button[] optionButtons;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private GameObject select, unequip, buy, oneTime, unequipGray, notEnough;
    public SkinOption[] skinOptions;
    private SkinClass currentClass;
    private Skin[] currentSkins;
    private Skin currentSkin;

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

        if (PlayerData.Ins.equippedSkins.Count == 0)
        {
            currentClass = SkinClass.Hair;
            currentSkins = SkinManager.Ins.dictSkinClass[currentClass];
            ChooseSkin(0);
        }
        else
        {
            currentClass = SkinManager.Ins.GetSkinClass(PlayerData.Ins.equippedSkins[0]);
            currentSkins = SkinManager.Ins.dictSkinClass[currentClass];
            UpdateBuff(currentSkins[0].data.skinBuff);
        }

        ShowOption(currentClass);
    }

    private void ChooseSkin(int option)
    {
        DisableAllButton();

        currentSkin = currentSkins[option];
        player.EquipSkin(currentSkin.skinType);

        SkinData data = currentSkin.data;
        switch (data.locked)
        {
            case 0:
                if (PlayerData.Ins.equippedSkins.Contains(currentSkin.skinType))
                {
                    unequip.SetActive(true);
                }
                else
                {
                    select.SetActive(true);
                }
                break;
            case 1:
                buy.SetActive(true);
                price.text = data.price.ToString();
                oneTime.SetActive(true);
                break;
            case 2:
                unequipGray.SetActive(true);
                break;
        }

        UpdateBuff(data.skinBuff);
    }

    private void UpdateBuff(BuffData buff)
    {
        if (buff.buffClass == BuffClass.Add)
        {
            buffText.text = "+ " + buff.amount.ToString() + " " + buff.buffType.ToString();
        }
        else
        {
            buffText.text = "+ " + buff.amount.ToString() + "% " + buff.buffType.ToString();
        }
    }

    private void DisableAllButton()
    {
        select.SetActive(false);
        unequip.SetActive(false);
        buy.SetActive(false);
        oneTime.SetActive(false);
        unequipGray.SetActive(false);
        notEnough.SetActive(false);
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
        player.OnInit();
        Close();
    }

    public void BuyButton()
    {
        if (PlayerData.Ins.SpendGold(currentSkin.data.price))
        {
            currentSkin.data.locked = 0;
            SkinManager.Ins.lockedSkins.Remove(currentSkin.skinType);
            ShowOption(currentClass);
            UpdateGold();
            DisableAllButton();
            select.SetActive(true);
        }
        else
        {
            notEnough.SetActive(true);
        }
    }

    public void SelectButton()
    {
        PlayerData.Ins.EquipSkin(currentSkin.skinType);
        DisableAllButton();
        unequip.SetActive(true);
        ShowOption(currentClass);
    }

    public void UnequipButton()
    {
        PlayerData.Ins.UnequipSkin(currentSkin.skinType);
        DisableAllButton();
        select.SetActive(true);
        ShowOption(currentClass);
    }

    public void OneTimeButton()
    {
        PlayerData.Ins.UnlockOneTime(currentSkin.skinType);
        currentSkin.data.locked = 2;
        DisableAllButton();
        unequipGray.SetActive(true);
        ShowOption(currentClass);
        PlayerData.Ins.EquipSkin(currentSkin.skinType);
    }

    public void HairButton()
    {
        if (currentClass == SkinClass.Hair) return;

        currentClass = SkinClass.Hair;
        currentSkins = SkinManager.Ins.dictSkinClass[currentClass];
        ChooseSkin(0);
        ShowOption(currentClass);
    }

    public void PantButton()
    {
        if (currentClass == SkinClass.Pant) return;

        currentClass = SkinClass.Pant;
        currentSkins = SkinManager.Ins.dictSkinClass[currentClass];
        ChooseSkin(0);
        ShowOption(currentClass);
    }
}
