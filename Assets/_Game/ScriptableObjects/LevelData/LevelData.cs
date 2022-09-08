using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    // Player Base Status
    public float playerRunSpeed;
    public float playerAttackSpeed;
    public float playerAttackRange;

    // Enemy Base Status
    public float enemyRunSpeed;
    public float enemyAttackSpeed;
    public float enemyAttackRange;
}
