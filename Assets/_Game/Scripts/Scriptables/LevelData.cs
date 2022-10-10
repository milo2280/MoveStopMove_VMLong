using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable/Level Data")]
public class LevelData : ScriptableObject
{
    public int level;
    public int botCount;
}
