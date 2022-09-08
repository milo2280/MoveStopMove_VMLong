using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : GameUnit
{
    public Transform indicatorTransform;
    public Transform arrowTransform;
    public Image pointImage;
    public Text pointText;

    public bool isEnabled { get; private set; }

    private Enemy enemy;

    private Vector3 direction;
    private float rad, deg, PosX, PosY, tanRad;

    private const float CIRCLE_R = 50f;
    private static readonly float MAX_X = Screen.width / 2 - 80f;
    private static readonly float MAX_Y = Screen.height / 2 - 80f;
    private static readonly float SCREEN_RATIO = MAX_Y / MAX_X;

    private void Update()
    {
        if (isEnabled) UpdatePos();
    }

    public void OnInit(Enemy enemy)
    {
        EnableIndicator();
        this.enemy = enemy;
        indicatorTransform.localPosition = Vector3.zero;
    }

    public void EnableIndicator()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            arrowTransform.gameObject.SetActive(true);
            pointImage.gameObject.SetActive(true);
        }
    }

    public void DisableIndicator()
    {
        if (isEnabled)
        {
            isEnabled = false;
            arrowTransform.gameObject.SetActive(false);
            pointImage.gameObject.SetActive(false);
        }
    }

    private void UpdatePos()
    {
        direction = enemy.charTransform.position - LevelManager.Ins.playerPos;
        rad = Mathf.Atan2(direction.z, direction.x);

        UpdateIndicatorPos();
        UpdateArrowPos();
    }

    private void UpdateIndicatorPos()
    {
        if (rad >= 0 && rad <= Mathf.PI)
        {
            if (Mathf.Abs(rad - Mathf.PI / 2) <= Constant.ZERO)
            {
                PosX = 0f;
                PosY = MAX_Y;
            }
            else
            {
                tanRad = Mathf.Tan(rad);

                if (tanRad >= 0 && tanRad <= SCREEN_RATIO)
                {
                    PosX = MAX_X;
                    PosY = PosX * tanRad;
                }
                else if (tanRad >= SCREEN_RATIO || tanRad <= -SCREEN_RATIO)
                {
                    PosY = MAX_Y;
                    PosX = PosY / tanRad;
                }
                else
                {
                    PosX = -MAX_X;
                    PosY = PosX * tanRad;
                }
            }
        }
        else
        {
            if (Mathf.Abs(rad - Mathf.PI / 2) <= Constant.ZERO)
            {
                PosX = 0f;
                PosY = -MAX_Y;
            }
            else
            {
                tanRad = Mathf.Tan(rad);

                if (tanRad >= 0 && tanRad <= SCREEN_RATIO)
                {
                    PosX = -MAX_X;
                    PosY = PosX * tanRad;
                }
                else if (tanRad >= SCREEN_RATIO || tanRad <= -SCREEN_RATIO)
                {
                    PosY = -MAX_Y;
                    PosX = PosY / tanRad;
                }
                else
                {
                    PosX = MAX_X;
                    PosY = PosX * tanRad;
                }
            }
        }

        indicatorTransform.localPosition = new Vector3(PosX, PosY, 0f);
    }

    private void UpdateArrowPos()
    {
        deg = rad * Mathf.Rad2Deg;
        arrowTransform.rotation = Quaternion.Euler(0f, 0f, deg - 90f);
        arrowTransform.localPosition = new Vector3(CIRCLE_R * Mathf.Cos(rad), CIRCLE_R * Mathf.Sin(rad), 0f);
    }

    private void OnDisable()
    {
        this.enemy = null;
    }
}
