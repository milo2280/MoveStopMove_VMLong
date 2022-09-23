using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadBar : MonoBehaviour
{
    public Transform m_Transform;

    public Image score;
    public Text scoreText, nameText;

    private Quaternion savedRotation = Quaternion.Euler(315f, 180f, 0f);
    private bool barEnabled;

    private void LateUpdate()
    {
        FreezeRotation();
        GameStateTransition();
    }

    public void EnableBar()
    {
        nameText.gameObject.SetActive(true);
        score.gameObject.SetActive(true);
    }

    public void DisableBar()
    {
        nameText.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
    }

    private void FreezeRotation()
    {
        m_Transform.rotation = savedRotation;
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
        nameText.text = name;
    }

    public void ChangeScore(int point)
    {
        scoreText.text = point.ToString();
    }

    public void SetColor(Color color)
    {
        nameText.color = color;
        score.color = color;
    }
}
