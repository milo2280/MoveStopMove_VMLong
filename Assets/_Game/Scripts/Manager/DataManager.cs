using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public TextAsset nameFile;
    public Material[] colors;

    private int colorIndex, colorCount;
    private List<int> usedColors = new List<int>();

    public bool isMute, isVibrate;

    private const string MUTE = "mute";
    private const string VIBRATE = "vibrate";

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

        NamesList namesList = JsonUtility.FromJson<NamesList>(nameFile.text);
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

        return colors[colorIndex].color;
    }

    public string GetDescription(System.Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }

    public void LoadData()
    {
        PlayerData.Ins.LoadData();

        if (PlayerPrefs.GetInt(MUTE) == 0) isMute = false;
        else isMute = true;

        if (PlayerPrefs.GetInt(VIBRATE) == 0) isVibrate = false;
        else isVibrate = true;
    }

    public void SaveData()
    {
        PlayerData.Ins.SaveData();

        if (isMute) PlayerPrefs.SetInt(MUTE, 1);
        else PlayerPrefs.SetInt(MUTE, 0);

        if (isVibrate) PlayerPrefs.SetInt(VIBRATE, 1);
        else PlayerPrefs.SetInt(VIBRATE, 0);
    }
}


