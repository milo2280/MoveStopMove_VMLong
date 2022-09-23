using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    public Text placeHolder, goldText;

    public GameObject soundOn, soundOff;
    public GameObject vibrateOn, vibrateOff;

    public RectTransform fillTransform;
    public GameObject[] ranks;

    private bool isSoundOn, isVibrateOn;

    private int[] goals = { 0, 100, 500, 1000 };
    private GameObject currentRank;
    private float currentScore, prevGoal, currentGoal, currentWidth;

    private const float MAX_WIDTH = 220f;
    private const float MIN_WIDTH = 28f;
    private const float HEIGHT = 28f;

    private void OnEnable()
    {
        placeHolder.text = PlayerData.Ins.playerName;
        goldText.text = PlayerData.Ins.gold.ToString();
        UpdateRank();
    }

    public void ReadStringInput(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            name = placeHolder.text;
        }

        placeHolder.text = name;
        PlayerData.Ins.SetPlayerName(name);
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

    private void UpdateRank()
    {
        currentScore = PlayerData.Ins.totalScore;

        if (currentScore == 0)
        {
            currentRank = ranks[0];
            fillTransform.sizeDelta = new Vector2(0, HEIGHT);
        }

        for (int i = 0; i < ranks.Length; i++)
        {
            if (currentScore > goals[i] && currentScore <= goals[i + 1])
            {
                currentGoal = goals[i + 1];
                prevGoal = goals[i];
                currentRank = ranks[i];
                ChangeFillWidth();
                break;
            }
        }

        currentRank.SetActive(true);
    }

    private void ChangeFillWidth()
    {
        currentWidth = MIN_WIDTH;
        currentWidth += (MAX_WIDTH - MIN_WIDTH) * (currentScore - prevGoal) / (currentGoal - prevGoal);
        fillTransform.sizeDelta = new Vector2(currentWidth, HEIGHT);
    }

    private void OnDisable()
    {
        currentRank.SetActive(false);
    }
}
