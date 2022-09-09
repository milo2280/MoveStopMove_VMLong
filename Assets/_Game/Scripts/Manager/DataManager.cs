using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Weapon[] weapons;
    public GameUnit[] bullets;
    public Color[] colors = { Color.black, Color.cyan, Color.gray, Color.green, Color.grey, Color.red, Color.blue };
}
