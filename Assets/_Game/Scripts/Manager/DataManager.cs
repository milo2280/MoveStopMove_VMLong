using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Weapon[] weapons;
    public GameUnit[] bullets;

    private Color[] colors = { 
    new Color(200f/255f, 0f, 200f/255f),
    new Color(100f/255f, 0f, 200f/255f),
    new Color(0f, 100f/255f, 200f/255f),
    new Color(0f, 200f/255f, 100f/255f),
    new Color(100f/255f, 200f/255f, 0f),
    Color.black, Color.gray, Color.green, Color.red, Color.blue };
    private int colorIndex, colorCount;
    private List<int> usedColors = new List<int>();

    private class NamesList
    {
        public List<string> names;
    }
    private int nameIndex, nameCount;
    private List<int> usedNames = new List<int>();
    private List<string> names = new List<string>();

    private void Awake()
    {
        colorCount = colors.Length;

        TextAsset textAsset = Resources.Load("Text/NamesList") as TextAsset;
        NamesList namesList = JsonUtility.FromJson<NamesList>(textAsset.text);
        names = namesList.names;
        nameCount = names.Count;
    }

    public string GetRandomName()
    {
        if (usedNames.Count == nameCount) usedNames.Clear();

        nameIndex = Random.Range(0, nameCount);
        while (usedNames.Contains(nameIndex))
        {
            nameIndex = Random.Range(0, nameCount);
        }
        usedNames.Add(nameIndex);

        return names[nameIndex];
    }

    public Color GetRandomColor()
    {
        if (usedColors.Count == colorCount) usedColors.Clear();

        colorIndex = Random.Range(0, colorCount);
        while (usedColors.Contains(colorIndex))
        {
            colorIndex = Random.Range(0, colorCount);
        }
        usedColors.Add(colorIndex);

        return colors[colorIndex];
    }
}


