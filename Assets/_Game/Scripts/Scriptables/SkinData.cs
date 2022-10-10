using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "Scriptable/Skin Data")]
public class SkinData : ScriptableObject
{
    public int price;
    public BuffData skinBuff;
    public int locked;
    public Material material;
}
