using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : GameUnit
{
    public Text pointText;
    public Image pointImage;
    public Image arrowImage;
    public Transform arrowTransform;
    public Transform indicatorTransform;

    private Camera mainCam;
    private Enemy enemy;
    private Vector3 viewPos;
    private float x, y, z, px, py, pz, ratio;
    private float rad, deg, PosX, PosY;

    private const float CIRCLE_R = 50f;
    private static readonly float MAX_X = Screen.width / 2 - 80f;
    private static readonly float MAX_Y = Screen.height / 2 - 80f;

    private void Update()
    {
        IndicateEnemy();
    }

    public void OnInit(Enemy enemy)
    {
        this.enemy = enemy;
        mainCam = LevelManager.Ins.mainCam;
        indicatorTransform.localPosition = Vector3.zero;
    }

    private void SetActive(bool isActive)
    {
        pointImage.gameObject.SetActive(isActive);
        arrowImage.gameObject.SetActive(isActive);
    }

    private bool IsOnScreen()
    {
        viewPos = mainCam.WorldToViewportPoint(enemy.myTransform.position);

        if ((viewPos.x > 0 && viewPos.x < 1) && (viewPos.y > 0 && viewPos.y < 1))
        {
            SetActive(false);
            return true;
        }

        SetActive(true);
        viewPos -= new Vector3(0.5f, 0.5f, 0f);

        return false;
    }

    private void IndicateEnemy()
    {
        if (!IsOnScreen())
        {
            UpdateIndicatorPos();
        }
    }

    private void UpdateIndicatorPos()
    {
        x = viewPos.x;
        y = viewPos.y;
        z = viewPos.z;

        pz = 1f;

        if (Mathf.Abs(z - 0f) > Constant.ZERO)
        {
            pz = z / Mathf.Abs(z);
        }

        if (Mathf.Abs(x - 0f) <= Constant.ZERO)
        {
            PosX = 0f;
            PosY = MAX_Y * y / Mathf.Abs(y) * pz;
        }

        if (Mathf.Abs(y - 0f) <= Constant.ZERO)
        {
            PosY = 0f;
            PosX = MAX_X * x / Mathf.Abs(x) * pz;
        }

        px = x / Mathf.Abs(x);
        py = y / Mathf.Abs(y);
        ratio = Mathf.Abs(x / y);

        if (ratio > 1)
        {
            PosX = MAX_X * px * pz;
            PosY = MAX_Y / ratio * py * pz;
        }
        else
        {
            PosY = MAX_Y * py * pz;
            PosX = MAX_X * ratio * px * pz;
        }

        indicatorTransform.localPosition = new Vector3(PosX, PosY, 0f);

        // Arrow Pos
        rad = Mathf.Atan2(y * pz, x * pz);
        deg = rad * Mathf.Rad2Deg;
        arrowTransform.rotation = Quaternion.Euler(0f, 0f, deg - 90f);
        arrowTransform.localPosition = new Vector3(CIRCLE_R * Mathf.Cos(rad), CIRCLE_R * Mathf.Sin(rad), 0f);
    }

    public void ChangeScore(int point)
    {
        pointText.text = point.ToString();
    }

    public void SetColor(Color color)
    {
        pointImage.color = color;
        arrowImage.color = color;
    }
}
