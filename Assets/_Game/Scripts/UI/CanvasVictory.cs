using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasVictory : UICanvas
{
    public TextMeshProUGUI gold;
    public GameObject nextLevelButton;

    private float timer;
    private bool isContinueActive;

    private void OnEnable()
    {
        timer = 0;
        isContinueActive = false;
        nextLevelButton.SetActive(false);
        gold.text = LevelManager.Ins.playerScore.ToString();
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
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        UIManager.Ins.OpenUI(UIID.UICOffer);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.BackHome();
        Close();
    }

    public void TripleReward()
    {
        PlayerData.Ins.ReceiveGold(2 * LevelManager.Ins.playerScore);
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        UIManager.Ins.OpenUI(UIID.UICOffer);
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.BackHome();
        Close();

    }

    public void ScreenShotButton()
    {
        Debug.Log("Screenshot");
    }
}
