using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public GameObject soundOn, soundOff, vibrateOn, vibrateOff;

    private void OnEnable()
    {
        soundOn.SetActive(!DataManager.Ins.isMute);
        soundOff.SetActive(DataManager.Ins.isMute);

        vibrateOn.SetActive(DataManager.Ins.isVibrate);
        vibrateOff.SetActive(!DataManager.Ins.isVibrate);
    }

    public void SoundToggleButton()
    {
        SoundManager.Ins.ToggleSound();
        soundOn.SetActive(!DataManager.Ins.isMute);
        soundOff.SetActive(DataManager.Ins.isMute);
    }

    public void VibrateToggleButton()
    {
        DataManager.Ins.isVibrate = !DataManager.Ins.isVibrate;
        vibrateOn.SetActive(DataManager.Ins.isVibrate);
        vibrateOff.SetActive(!DataManager.Ins.isVibrate);
    }

    public void ContinueButton()
    {
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
        Close();
    }

    public void HomeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.RestartLevel();
        LevelManager.Ins.BackHome();
        Close();
    }
}
