using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffClass
{
    Add,
    Percentage
}

public enum BuffType
{
    Range,
    AttackSpeed,
    MoveSpeed,
    Gold,
}

[CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObjects/Buff", order = 1)]
public class Buff : ScriptableObject
{
    public BuffClass buffClass;
    public BuffType buffType;
    public int buffAmount;
    public string buffName;
}
