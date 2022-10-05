using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasFail : UICanvas
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI killerText;
    public TextMeshProUGUI gold;
    public GameObject continueButton;
    public GameObject[] currentZones;
    public GameObject[] nextZones;
    public Text currentZone, nextZone;

    public RectTransform current, best;

    private const float MAX_WIDTH = 220f;
    private const float MIN_WIDTH = 16f;
    private const float HEIGHT = 24f;

    private float timer;
    private bool isContinueActive;

    private void OnEnable()
    {
        timer = 0;
        isContinueActive = false;
        continueButton.SetActive(false);
        DisableAllZone();
        currentZones[LevelManager.Ins.currentLevel.level - 1].SetActive(true);
        nextZones[LevelManager.Ins.currentLevel.level - 1].SetActive(true);
        ShowProgress();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > Constant.DELAY_BUTTON && !isContinueActive)
        {
            continueButton.SetActive(true);
            isContinueActive = true;
        }
    }

    public void OnInit()
    {
        rankText.text = "#" + LevelManager.Ins.rank.ToString();
        killerText.text = LevelManager.Ins.killer;
        gold.text = LevelManager.Ins.goldReceive.ToString();
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

    private void ShowProgress()
    {
        float width = MIN_WIDTH;
        width += (MAX_WIDTH - MIN_WIDTH) * (LevelManager.Ins.currentLevel.botCount + 1 - PlayerData.Ins.bestRank) / (LevelManager.Ins.currentLevel.botCount + 1);

        best.sizeDelta = new Vector2(width, HEIGHT);

        width = MIN_WIDTH;
        width += (MAX_WIDTH - MIN_WIDTH) * (LevelManager.Ins.currentLevel.botCount + 1 - LevelManager.Ins.rank) / (LevelManager.Ins.currentLevel.botCount + 1);

        current.sizeDelta = new Vector2(width, HEIGHT);

        currentZone.text = "ZONE: " + LevelManager.Ins.currentLevel.level.ToString();
        nextZone.text = "ZONE: " + (LevelManager.Ins.currentLevel.level + 1).ToString();
    }
}
