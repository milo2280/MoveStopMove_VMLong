using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasVictory : UICanvas
{
    public TextMeshProUGUI gold;
    public GameObject nextLevelButton;
    public GameObject[] currentZones;
    public GameObject[] nextZones;
    public Text currentZone, nextZone;

    private float timer;
    private bool isContinueActive;

    private void OnEnable()
    {
        SoundManager.Ins.PlayAudio(AudioType.Win);
        timer = 0;
        isContinueActive = false;
        nextLevelButton.SetActive(false);
        gold.text = LevelManager.Ins.goldReceive.ToString();
        DisableAllZone();
        currentZones[LevelManager.Ins.currentLevel.level - 1].SetActive(true);
        nextZones[LevelManager.Ins.currentLevel.level - 1].SetActive(true);
        currentZone.text = "ZONE: " + (LevelManager.Ins.currentLevel.level - 1).ToString();
        nextZone.text = "ZONE: " + (LevelManager.Ins.currentLevel.level).ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > Constant.DELAY_BUTTON && !isContinueActive)
        {
            nextLevelButton.SetActive(true);
            isContinueActive = true;
        }
    }

    public void HomeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICOffer);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.RestartLevel();
        LevelManager.Ins.BackHome();
        Close();
    }

    public void TripleReward()
    {
        PlayerData.Ins.ReceiveGold(2 * LevelManager.Ins.goldReceive);
        UIManager.Ins.OpenUI(UIID.UICOffer);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.RestartLevel();
        LevelManager.Ins.BackHome();
        Close();

    }

    public void ScreenShotButton()
    {
        Debug.Log("Screenshot");
    }

    private void DisableAllZone()
    {
        for (int i = 0; i < currentZones.Length; i++)
        {
            currentZones[i].SetActive(false);
            nextZones[i].SetActive(false);
        }
    }
}
