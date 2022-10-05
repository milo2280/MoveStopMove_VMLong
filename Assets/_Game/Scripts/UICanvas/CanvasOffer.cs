using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOffer : UICanvas
{
    public GameObject LoseItButton;
    public Transform itemTransform;

    private Skin currentSkin;
    private Dictionary<SkinType, Skin> usedSkins = new Dictionary<SkinType, Skin>();
    private float timer;
    private bool isLoseItActive;

    private void OnEnable()
    {
        timer = 0;
        isLoseItActive = false;
        LoseItButton.SetActive(false);
        LevelManager.Ins.HidePlayer();

        if (SkinManager.Ins.lockedSkins.Count == 0) CloseButton();
        else
        {
            if (currentSkin != null) currentSkin.gameObject.SetActive(false);

            SkinType skinType = SkinManager.Ins.GetRandomLockedSkin();
            if (usedSkins.ContainsKey(skinType))
            {
                currentSkin = usedSkins[skinType];
                currentSkin.gameObject.SetActive(true);
            }
            else
            {
                currentSkin = Instantiate<Skin>(SkinManager.Ins.dictSkin[skinType], itemTransform);
                usedSkins.Add(currentSkin.skinType, currentSkin);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > Constant.DELAY_BUTTON && !isLoseItActive)
        {
            isLoseItActive = true;
            LoseItButton.SetActive(true);
        }
    }

    private void OnDisable()
    {
        LevelManager.Ins.ShowPlayer();
    }

    public void ClaimButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        PlayerData.Ins.UnlockOneTime(currentSkin.skinType);
        currentSkin.data.locked = 2;
        PlayerData.Ins.EquipSkin(currentSkin.skinType);
        Close();
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        Close();
    }
}
