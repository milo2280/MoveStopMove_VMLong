using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum BuffClass
{
    Add,
    Percent
}

public enum BuffType
{
    [Description("Range")] Range = 0,
    [Description("Attack Speed")] AttackSpeed = 1,
    [Description("Move Speed")] MoveSpeed = 2,
    [Description("Gold")] Gold = 3,
}

[CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObjects/Buff", order = 1)]
public class Buff : ScriptableObject
{
    public BuffClass buffClass;
    public BuffType buffType;
    public float buffAmount;
}