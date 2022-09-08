using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public Transform indicatorParent;
    public Transform enemyParent;
    public Transform[] bulletParents;

    public GameUnit indicatorPrefab;
    public GameUnit enemyPrefab;

    private void Awake()
    {
        SimplePool.Preload(indicatorPrefab, 7, indicatorParent);
        SimplePool.Preload(enemyPrefab, 7, enemyParent);

        for (int i = 0; i < bulletParents.Length; i++)
        {
            SimplePool.Preload(DataManager.Ins.bullets[i], 10, bulletParents[i]);
        }
    }
}
