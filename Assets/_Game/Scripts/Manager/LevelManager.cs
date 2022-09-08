using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Camera mainCam;
    public CameraFollow cameraFollow;
    public Vector3 playerPos;

    public GameUnit enemyPrefab, indicatorPrefab;

    private int spawnCounter = 0;
    private int deadCounter = 0;
    private List<Enemy> listActiveEnemy = new List<Enemy>();
    private Dictionary<Enemy, Indicator> dictIndicator = new Dictionary<Enemy, Indicator>();

    private const int START_ENEMY_NUMBER = 5;
    private const int MAX_ENEMY_NUMBER = 10;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        EnemyIndicator();
        UpdatePlayerInfo();
    }

    private void OnInit()
    {
        for (int i = 0; i < START_ENEMY_NUMBER; i++)
        {
            SpawnEnemy();
        }
    }

    private void OnReset()
    {
        player.OnInit();
        SimplePool.CollectAll();
        listActiveEnemy.Clear();
        dictIndicator.Clear();
        OnInit();
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(Constant.MAX_X, -Constant.MAX_X);
        float z = Random.Range(Constant.MAX_Z, -Constant.MAX_Z);
        Vector3 randomPos = new Vector3(x, 0f, z);
        Quaternion randomRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);
        Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab, randomPos, randomRot);
        enemy.OnInit();

        Indicator indicator = SimplePool.Spawn<Indicator>(indicatorPrefab, Vector3.zero, Quaternion.identity);
        indicator.OnInit(enemy);

        listActiveEnemy.Add(enemy);
        dictIndicator.Add(enemy, indicator);

        spawnCounter++;
    }

    public void AEnemyDead(Enemy enemy)
    {
        SimplePool.Despawn(dictIndicator[enemy]);
        dictIndicator.Remove(enemy);
        listActiveEnemy.Remove(enemy);

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

    public void MainMenu()
    {
        cameraFollow.MainMenuPos();
        OnReset();
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

    private void EnemyIndicator()
    {
        for (int i = 0; i < listActiveEnemy.Count; i++)
        {
            Enemy enemy = listActiveEnemy[i];

            if (!IsOnScreen(enemy))
            {
                EnableIndicator(enemy);
            }
            else
            {
                DisableIndicator(enemy);
            }
        }
    }

    private bool IsOnScreen(Enemy enemy)
    {
        Vector3 viewPos = mainCam.WorldToViewportPoint(enemy.charTransform.position);

        if ((viewPos.x > 0 && viewPos.x < 1) && (viewPos.y > 0 && viewPos.y < 1))
        {
            return true;
        }

        return false;
    }

    private void EnableIndicator(Enemy enemy)
    {
        if (dictIndicator.ContainsKey(enemy))
        {
            dictIndicator[enemy].EnableIndicator();
        }
    }

    private void DisableIndicator(Enemy enemy)
    {
        if (dictIndicator.ContainsKey(enemy))
        {
            dictIndicator[enemy].DisableIndicator();
        }
    }

    private void UpdatePlayerInfo()
    {
        playerPos = player.charTransform.position;
    }
}
