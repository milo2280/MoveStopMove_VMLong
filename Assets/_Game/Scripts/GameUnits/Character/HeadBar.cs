using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadBar : MonoBehaviour
{
    public Transform m_Transform;

    public Image score;
    public Text scoreText, nameText;

    private static readonly Quaternion ROTATION = Quaternion.Euler(315f, 180f, 0f);

    private void LateUpdate()
    {
        m_Transform.rotation = ROTATION;
        SetActive(GameManager.Ins.IsState(GameState.Gameplay));
    }

    public void SetActive(bool active)
    {
        nameText.gameObject.SetActive(active);
        score.gameObject.SetActive(active);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetColor(Color color)
    {
        nameText.color = color;
        score.color = color;
    }
}
