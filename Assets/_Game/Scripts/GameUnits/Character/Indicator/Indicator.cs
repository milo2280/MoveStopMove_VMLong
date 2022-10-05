using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : GameUnit
{
    public Transform m_Transform;
    public Transform arrowTransform;
    public Image arrowImage, scoreImage;
    public Text scoreText;

    private Camera mainCam;
    private Enemy enemy;
    private Vector3 viewPos;
    private float x, y, z, signX, signY, signZ, ratio;
    private float rad, deg, PosX, PosY;

    private const float RADIUS = 50f;
    private static readonly Vector3 offset = new Vector3(0.5f, 0.5f, 0f);
    private static readonly float MAX_X = Screen.width / 2 - 80;
    private static readonly float MAX_Y = Screen.height / 2 - 80;

    private void Awake()
    {
        mainCam = LevelManager.Ins.mainCam;
    }

    private void Update()
    {
        IndicateEnemy();
    }

    public void OnInit(Enemy enemy)
    {
        this.enemy = enemy;
    }

    private void IndicateEnemy()
    {
        if (!IsOnScreen())
        {
            UpdateIndicatorPos();
        }
    }

    private bool IsOnScreen()
    {
        viewPos = mainCam.WorldToViewportPoint(enemy.m_Transform.position);

        if ((viewPos.x > 0 && viewPos.x < 1) && (viewPos.y > 0 && viewPos.y < 1))
        {
            SetActive(false);
            return true;
        }
        else
        {
            viewPos -= offset;
            SetActive(true);
            return false;
        }
    }

    private void SetActive(bool isActive)
    {
        scoreImage.gameObject.SetActive(isActive);
        arrowImage.gameObject.SetActive(isActive);
    }


    private void UpdateIndicatorPos()
    {
        x = viewPos.x;
        y = viewPos.y;
        z = viewPos.z;

        signX = Mathf.Sign(x);
        signY = Mathf.Sign(y);
        signZ = Mathf.Sign(z);

        if (Mathf.Approximately(x, 0f))
        {
            PosX = 0f;
            PosY = MAX_Y * signY * signZ;
        }
        else if (Mathf.Approximately(y, 0f))
        {
            PosY = 0f;
            PosX = MAX_X * signX * signZ;
        }
        else
        {
            ratio = Mathf.Abs(x / y);

            if (ratio > 1)
            {
                PosX = MAX_X * signX * signZ;
                PosY = MAX_Y / ratio * signY * signZ;
            }
            else
            {
                PosY = MAX_Y * signY * signZ;
                PosX = MAX_X * ratio * signX * signZ;
            }
        }

        m_Transform.localPosition = new Vector3(PosX, PosY, 0f);

        // Arrow Pos
        rad = Mathf.Atan2(y * signZ, x * signZ);
        deg = rad * Mathf.Rad2Deg;
        arrowTransform.rotation = Quaternion.Euler(0f, 0f, deg - 90f);
        arrowTransform.localPosition = new Vector3(RADIUS * Mathf.Cos(rad), RADIUS * Mathf.Sin(rad), 0f);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetColor(Color color)
    {
        scoreImage.color = color;
        arrowImage.color = color;
    }
}
