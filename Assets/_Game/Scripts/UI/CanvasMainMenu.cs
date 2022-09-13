using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    public Text placeHolder;
    public PlayerData playerData;

    public GameObject soundOn, soundOff;
    public GameObject vibrateOn, vibrateOff;

    private bool isSoundOn, isVibrateOn;

    private void OnEnable()
    {
        placeHolder.text = playerData.playerName;
    }

    public void ReadStringInput(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            name = placeHolder.text;
        }

        placeHolder.text = name;
        playerData.playerName = name;
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
        UIManager.Ins.OpenUI(UIID.UICShopWeapon);
        LevelManager.Ins.HidePlayer();
        Close();
    }

    public void SkinButton()
    {
        Debug.Log("Skin");
    }

    public void RemoveAdButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMoneyShop);
        LevelManager.Ins.HidePlayer();
        Close();
    }

    public void VibrateButton()
    {
        Debug.Log("Vibrate");
        vibrateOn.SetActive(isVibrateOn);
        vibrateOff.SetActive(!isVibrateOn);
        isVibrateOn = !isVibrateOn;
    }

    public void SoundButton()
    {
        SoundManager.Ins.SoundOnOff();
        soundOn.SetActive(isSoundOn);
        soundOff.SetActive(!isSoundOn);
        isSoundOn = !isSoundOn;
    }
}
