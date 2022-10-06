using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public Transform indicatorParent;
    public Transform enemyParent;
    public Transform hitVFXParent, reviveVFXParent;
    public Transform[] bulletParents;

    public GameUnit indicatorPrefab;
    public GameUnit enemyPrefab;
    public ParticleSystem hitVFX, reviveVFX;

    private void Awake()
    {
        SimplePool.Preload(indicatorPrefab, 10, indicatorParent);
        SimplePool.Preload(enemyPrefab, 10, enemyParent);

        for (int i = 0; i < WeaponManager.Ins.bullets.Length; i++)
        {
            SimplePool.Preload(WeaponManager.Ins.bullets[i], 10, bulletParents[i]);
        }

        ParticlePool.Preload(hitVFX, 10, hitVFXParent);
        ParticlePool.Preload(reviveVFX, 1, reviveVFXParent);
    }
}
