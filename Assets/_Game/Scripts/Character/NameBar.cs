using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameBar : MonoBehaviour
{
    public Text charName;
    public Image pointBG;
    public Transform nameBarTrans;

    private Quaternion savedRotation = Quaternion.Euler(315f, 180f, 0f);
    private bool barEnabled;

    private void LateUpdate()
    {
        FreezeRotation();
        GameStateTransition();
    }

    public void EnableBar()
    {
        charName.gameObject.SetActive(true);
        pointBG.gameObject.SetActive(true);
    }

    public void DisableBar()
    {
        charName.gameObject.SetActive(false);
        pointBG.gameObject.SetActive(false);
    }

    private void FreezeRotation()
    {
        nameBarTrans.rotation = savedRotation;
    }

    private void GameStateTransition()
    {
        if (!barEnabled)
        {
            if (GameManager.Ins.IsState(GameState.Gameplay))
            {
                EnableBar();
                barEnabled = true;
            }
        }
        else
        {
            if (GameManager.Ins.IsState(GameState.MainMenu))
            {
                DisableBar();
                barEnabled = false;
            }
        }
    }

    public void SetName(string name)
    {
        charName.text = name;
    }
}
