using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public GameObject soundOn, soundOff, vibrateOn, vibrateOff;

    private void OnEnable()
    {
        ChangeSoundToggle();
    }

    public void SoundToggleButton()
    {
        SoundManager.Ins.SoundOnOff();
        ChangeSoundToggle();
    }

    public void VibrateToggleButton()
    {
        Debug.Log("Vibrate");
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
        LevelManager.Ins.BackHome();
        Close();
    }

    private void ChangeSoundToggle()
    {
        soundOn.SetActive(SoundManager.Ins.isSoundOn);
        soundOff.SetActive(!SoundManager.Ins.isSoundOn);
    }
}
