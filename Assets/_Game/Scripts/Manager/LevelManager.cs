using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Vector3 playerPos { get; private set; }
    public float playerRange;
    public GameUnit enemyPrefab;

    private int counter = 7;

    private const int START_ENEMY_NUMBER = 7;
    private const int MAX_ENEMY_NUMBER = 50;

    private void Update()
    {
        playerPos = player.charTransform.position;
    }

    //private void OnInit()
    //{
    //    for (int i = 0; i < START_ENEMY_NUMBER; i++)
    //    {
    //        SpawnEnemy();
    //    }
    //}

    //private void SpawnEnemy()
    //{
    //    Vector3 randomPos = new Vector3(Random.Range(Constant.MAX_X, -Constant.MAX_X), 0f, Random.Range(Constant.MAX_Z, -Constant.MAX_Z));
    //    Quaternion randomRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);

    //    SimplePool.Spawn(enemyPrefab, randomPos, randomRot);
    //}

    //public void AEnemyDead()
    //{
    //    if (counter < MAX_ENEMY_NUMBER)
    //    {
    //        SpawnEnemy();
    //    }
    //}
}
