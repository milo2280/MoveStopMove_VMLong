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
        SimplePool.Preload(indicatorPrefab, 10, indicatorParent);
        SimplePool.Preload(enemyPrefab, 10, enemyParent);

        for (int i = 0; i < bulletParents.Length; i++)
        {
            SimplePool.Preload(DataManager.Ins.bullets[i], 5, bulletParents[i]);
        }
    }
}
