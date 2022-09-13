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

    private LevelData currentLevel;
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
        spawnCounter = 0;
        deadCounter = 0;
        player.OnInit();
        UpdateEnemyRemain();
        cameraFollow.OnReset();
        indicatorHolder.OnReset();
        SimplePool.CollectAll();
    }

    public void OnRevive()
    {
        player.OnRevive();
    }

    private void SpawnEnemy()
    {
        // Not spawn in player range
        float x = Random.Range(8f, 2 * Constant.MAX_X - 8f);
        if (x > Constant.MAX_X) x = Constant.MAX_X - (x + 8f);
        float z = Random.Range(8f, 2 * Constant.MAX_Z - 8f);
        if (z > Constant.MAX_Z) z = Constant.MAX_Z - (z + 8f);
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
            Victory();
        }
    }

    public void BackHome()
    {
        RestartLevel();
        indicatorHolder.HideAllIndicator();
        cameraFollow.MainMenuPos();
    }

    public void PlayGame()
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

    public void Victory()
    {
        if (UIManager.Ins.IsOpened(UIID.UICGamePlay))
        {
            UIManager.Ins.GetUI<CanvasGameplay>(UIID.UICGamePlay).Victory();
        }
    }

    public void Fail()
    {
        if (UIManager.Ins.IsOpened(UIID.UICGamePlay))
        {
            UIManager.Ins.GetUI<CanvasGameplay>(UIID.UICGamePlay).Fail();
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

    public void HidePlayer()
    {
        player.gameObject.SetActive(false);
    }

    public void ShowPlayer()
    {
        player.gameObject.SetActive(true);
    }
}
