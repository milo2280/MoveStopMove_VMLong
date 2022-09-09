using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Camera mainCam;
    public CameraFollow cameraFollow;
    public IndicatorHolder indicatorHolder;
    public GameUnit enemyPrefab;
    public LevelData[] levelDatas;

    public Vector3 playerPos => player.myTransform.position;

    private LevelData currentLevel;
    private bool isLevelEnded;
    private int spawnCounter;
    private int deadCounter;

    private const int START_ENEMY_NUMBER = 5;
    private const int MAX_ENEMY_NUMBER = 10;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        for (int i = 0; i < START_ENEMY_NUMBER; i++)
        {
            SpawnEnemy();
        }
    }

    public void OnReset()
    {
        isLevelEnded = false;
        spawnCounter = 0;
        deadCounter = 0;
        player.OnInit();
        UpdateEnemyRemain();
        cameraFollow.OnReset();
        indicatorHolder.OnReset();
        SimplePool.CollectAll();
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(Constant.MAX_X, -Constant.MAX_X);
        float z = Random.Range(Constant.MAX_Z, -Constant.MAX_Z);
        Vector3 randomPos = new Vector3(x, 0f, z);
        Quaternion randomRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);
        Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab, randomPos, randomRot);
        enemy.OnInit();
        indicatorHolder.AddIndicator(enemy);
        spawnCounter++;
    }

    public void AEnemyDead(Enemy enemy)
    {
        indicatorHolder.RemoveIndicator(enemy);

        if (spawnCounter < MAX_ENEMY_NUMBER)
        {
            SpawnEnemy();
        }

        deadCounter++;
        UpdateEnemyRemain();

        if (deadCounter == MAX_ENEMY_NUMBER)
        {
            EndLevel(true);
        }
    }

    public void MainMenu()
    {
        OnReset();
        OnInit();
        indicatorHolder.HideAllIndicator();
        cameraFollow.MainMenuPos();
    }

    public void Gameplay()
    {
        indicatorHolder.ShowAllIndicator();
        cameraFollow.GameplayPos();
        UpdateEnemyRemain();
    }

    public void RestartLevel()
    {
        OnReset();
        OnInit();
    }
    public void SetPlayerName(string name)
    {
        player.SetName(name);
    }

    public void EndLevel(bool isWin)
    {
        if (isLevelEnded) return;
        isLevelEnded = true;

        if (isWin)
        {
            if (UIManager.Ins.IsOpened(UIID.UICGamePlay))
            {
                UIManager.Ins.GetUI<CanvasGameplay>(UIID.UICGamePlay).Victory();
            }
        }
        else
        {
            if (UIManager.Ins.IsOpened(UIID.UICGamePlay))
            {
                UIManager.Ins.GetUI<CanvasGameplay>(UIID.UICGamePlay).Fail();
            }
        }
    }

    private void UpdateEnemyRemain()
    {
        if (UIManager.Ins.IsOpened(UIID.UICGamePlay))
        {
            CanvasGameplay canvas = UIManager.Ins.GetUI<CanvasGameplay>(UIID.UICGamePlay);
            canvas.UpdateEnemyRemain(MAX_ENEMY_NUMBER - deadCounter + 1);
        }
    }
}
