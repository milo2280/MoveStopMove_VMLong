using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Scriptable/Buff Data")]
public class BuffData : ScriptableObject
{
    public BuffClass buffClass;
    public BuffType buffType;
    public float amount;
}

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