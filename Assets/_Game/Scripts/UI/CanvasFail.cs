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

    private float timer;
    private bool isContinueActive;

    private void OnEnable()
    {
        timer = 0;
        isContinueActive = false;
        continueButton.SetActive(false);
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
        gold.text = LevelManager.Ins.playerScore.ToString();
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
