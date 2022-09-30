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
    public Transform[] SpawnPos;
    public GameObject[] listObstacle;

    public int rank;
    public string killer;
    public int playerScore => player.Score;

    public int deadCounter;

    private LevelData currentLevel;
    private int spawnCounter;
    private float timer;
    private bool isWin, isNewLevel, isPopUI;
    private int random;
    private List<int> used = new List<int>();

    private const int REVIVE_GOLD = 150;
    private const int START_ENEMY_NUMBER = 2;
    private const int MAX_ENEMY_NUMBER = 3;

    private void Awake()
    {
        currentLevel = levelDatas[PlayerData.Ins.currentLevel - 1];
    }

    private void Start()
    {
        OnInit();
        LoadLevel();
    }

    private void Update()
    {
        if (isPopUI)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                End();
            }
        }
    }

    private void LoadLevel()
    {
        EnableObstacle();
    }

    private void EnableObstacle()
    {
        switch (currentLevel.level)
        {
            case 1:
                for (int i = 0; i < listObstacle.Length; i++)
                {
                    listObstacle[i].SetActive(false);
                }

                break;

            case 2:
                listObstacle[0].SetActive(true);
                listObstacle[1].SetActive(true);
                listObstacle[2].SetActive(false);
                listObstacle[3].SetActive(false);

                break;

            case 3:
                for (int i = 0; i < listObstacle.Length; i++)
                {
                    listObstacle[i].SetActive(true);
                }

                break;

            default:
                break;
        }
    }

    private void OnInit()
    {
        if (isNewLevel)
        {
            LoadLevel();
            isNewLevel = false;
        }

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
        indicatorHolder.OnReset();
        SimplePool.CollectAll();
    }

    public bool OnRevive(bool isUseGold)
    {
        if (isUseGold)
        {
            if (PlayerData.Ins.SpendGold(REVIVE_GOLD))
            {
                player.OnRevive();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            player.OnRevive();
            return true;
        }
    }

    private void SpawnEnemy()
    {
        if (used.Count == SpawnPos.Length) used.Clear();
        random = Random.Range(0, SpawnPos.Length);
        while (used.Contains(random))
        {
            random = Random.Range(0, SpawnPos.Length);
        }
        used.Add(random);
        Vector3 randomPos = SpawnPos[random].position;
        Quaternion randomRot = Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f);
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

    public void BackHome()
    {
        player.ChangeAnim(Constant.ANIM_IDLE);
        cameraFollow.MenuVC();
        indicatorHolder.HideAllIndicator();
    }

    public void PlayGame()
    {
        indicatorHolder.ShowAllIndicator();
        cameraFollow.PlayVC();
        UpdateEnemyRemain();
    }

    public void SkinShop()
    {
        cameraFollow.SkinVC();
        player.ChangeAnim(Constant.ANIM_DANCE);
    }

    public void RestartLevel()
    {
        OnReset();
        OnInit();
    }

    public void EndLevel(bool isWin)
    {
        PlayerData.Ins.ReceiveGold(playerScore);
        PlayerData.Ins.AddTotalScore(playerScore);

        if (!isPopUI)
        {
            this.isWin = isWin;
        }
        else
        {
            this.isWin = false;
        }
        isPopUI = true;
    }

    private void End()
    {
        timer = 0;
        isPopUI = false;

        if (isWin)
        {
            if (currentLevel.level < 3)
            {
                currentLevel = levelDatas[currentLevel.level];
                PlayerData.Ins.currentLevel = currentLevel.level;
                isNewLevel = true;
            }

            if (UIManager.Ins.IsOpened(UIID.UICGamePlay))
            {
                UIManager.Ins.GetUI<CanvasGameplay>(UIID.UICGamePlay).Victory();
            }

            player.Win();
        }
        else
        {
            rank = MAX_ENEMY_NUMBER - deadCounter + 1;
            if (UIManager.Ins.IsOpened(UIID.UICGamePlay))
            {
                UIManager.Ins.GetUI<CanvasGameplay>(UIID.UICGamePlay).Fail(MAX_ENEMY_NUMBER - deadCounter);
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

    public void HidePlayer()
    {
        player.gameObject.SetActive(false);
    }

    public void ShowPlayer()
    {
        player.gameObject.SetActive(true);
        player.OnInit();
    }
}
