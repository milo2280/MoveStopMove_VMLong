using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public CameraFollow cameraFollow;

    public Vector3 playerPos { get; private set; }
    public float playerRange { get; private set; }

    public GameUnit enemyPrefab;

    private int spawnCounter = 0;
    private int deadCounter = 0;

    private const int START_ENEMY_NUMBER = 2;
    private const int MAX_ENEMY_NUMBER = 5;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        UpdatePlayerInfo();
    }

    private void OnInit()
    {
        for (int i = 0; i < START_ENEMY_NUMBER; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomPos = new Vector3(Random.Range(Constant.MAX_X, -Constant.MAX_X), 0f, Random.Range(Constant.MAX_Z, -Constant.MAX_Z));
        Quaternion randomRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);

        SimplePool.Spawn(enemyPrefab, randomPos, randomRot);

        spawnCounter++;
    }

    public void AEnemyDead()
    {
        if (spawnCounter < MAX_ENEMY_NUMBER)
        {
            SpawnEnemy();
        }

        deadCounter++;

        if (deadCounter == MAX_ENEMY_NUMBER)
        {
            EndLevel(true);
        }
    }

    private void UpdatePlayerInfo()
    {
        playerPos = player.charTransform.position;
        playerRange = player.GetAttackRange();
    }

    public void MainMenu()
    {
        cameraFollow.MainMenuPos();
    }

    public void Gameplay()
    {
        cameraFollow.GameplayPos();
    }

    public void SetPlayerName(string name)
    {
        player.SetName(name);
    }

    public void EndLevel(bool isWin)
    {
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
}
