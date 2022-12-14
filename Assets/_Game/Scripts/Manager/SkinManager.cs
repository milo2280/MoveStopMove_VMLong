using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinClass
{
    Hair = 100,
    Pant = 200,
    Shield = 300,
    SetFull = 400,
}

public enum SkinType
{
    Hair1 = 101,
    Hair2 = 102,
    Hair3 = 103,
    Hair4 = 104,
    Hair5 = 105,
    Hair6 = 106,
    Hair7 = 107,

    Pant1 = 201,
    Pant2 = 202,
    Pant3 = 203,
    Pant4 = 204,
    Pant5 = 205,
    Pant6 = 206,
    Pant7 = 207,
}

public class SkinManager : Singleton<SkinManager>
{
    public Skin[] hairs;
    public Skin[] pants;

    public List<SkinType> lockedSkins = new List<SkinType>();
    public Dictionary<SkinType, Skin> dictSkin = new Dictionary<SkinType, Skin>();
    public Dictionary<SkinClass, Skin[]> dictSkinClass = new Dictionary<SkinClass, Skin[]>();

    private void Awake()
    {
        dictSkinClass.Add(SkinClass.Hair, hairs);
        dictSkinClass.Add(SkinClass.Pant, pants);

        for (int i = 0; i < hairs.Length; i++)
        {
            dictSkin.Add(hairs[i].skinType, hairs[i]);
            if (hairs[i].data.locked == 1) lockedSkins.Add(hairs[i].skinType);
        }

        for (int i = 0; i < pants.Length; i++)
        {
            dictSkin.Add(pants[i].skinType, pants[i]);
            if (pants[i].data.locked == 1) lockedSkins.Add(pants[i].skinType);
        }
    }

    public SkinType GetRandomHair()
    {
        return hairs[Random.Range(0, hairs.Length)].skinType;
    }

    public SkinType GetRandomPant()
    {
        return pants[Random.Range(0, pants.Length)].skinType;
    }

    public SkinType GetRandomLockedSkin()
    {
        return lockedSkins[Random.Range(0, lockedSkins.Count)];
    }

    public SkinClass GetSkinClass(SkinType skinType)
    {
        int type = (int)skinType;
        return (SkinClass)(type - (type % 100));
    }
}


