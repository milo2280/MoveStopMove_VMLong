using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum WeaponClass
{
    [Description("HAMMER")] Hammer = 100,
    [Description("KNIFE")] Knife = 200,
    [Description("ICE-CREAM")] IceCream = 300,
}

public enum WeaponType
{
    Hammer1 = 101,
    Hammer2 = 102,

    Knife1 = 201,
    Knife2 = 202,

    IceCream1 = 301,
    IceCream2 = 302,
}

public class WeaponManager : Singleton<WeaponManager>
{
    public Bullet[] bullets;
    public Weapon[] weapons;
    public WeaponClassInfo[] infos;
    public WeaponItem[] weaponItems;
    public List<WeaponType> listWeaponType = new List<WeaponType>();
    public Dictionary<WeaponClass, WeaponClassInfo> dictClassInfo = new Dictionary<WeaponClass, WeaponClassInfo>();
    public Dictionary<WeaponType, Weapon> dictWeapon = new Dictionary<WeaponType, Weapon>();
    public Dictionary<WeaponType, WeaponItem> dictWeaponItem = new Dictionary<WeaponType, WeaponItem>();
    public Dictionary<WeaponClass, List<WeaponType>> dictWeaponClass = new Dictionary<WeaponClass, List<WeaponType>>();

    private void Awake()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            listWeaponType.Add(weaponItems[i].weaponType);
            dictWeapon.Add(weapons[i].weaponType, weapons[i]);
            dictWeaponItem.Add(weaponItems[i].weaponType, weaponItems[i]);
            ClassifyWeapon(weaponItems[i]);
        }

        for (int i = 0; i < infos.Length; i++)
        {
            dictClassInfo.Add(infos[i].weaponClass, infos[i]);
        }
    }

    private void ClassifyWeapon(WeaponItem weapon)
    {
        if (!dictWeaponClass.ContainsKey(weapon.weaponClass))
        {
            List<WeaponType> temp = new List<WeaponType>();
            dictWeaponClass.Add(weapon.weaponClass, temp);
        }

        dictWeaponClass[weapon.weaponClass].Add(weapon.weaponType);
    }

    public WeaponClass NextClass(WeaponClass currentClass)
    {
        WeaponClass nextClass = (WeaponClass)((int)currentClass + 100);
        if (dictWeaponClass.ContainsKey(nextClass))
        {
            return nextClass;
        }
        else
        {
            return currentClass;
        }
    }

    public WeaponClass PrevClass(WeaponClass currentClass)
    {
        WeaponClass prevClass = (WeaponClass)((int)currentClass - 100);
        if (dictWeaponClass.ContainsKey(prevClass))
        {
            return prevClass;
        }
        else
        {
            return currentClass;
        }
    }

    public WeaponType GetRandomType()
    {
        int rand = Random.Range(0, listWeaponType.Count - 1);
        return listWeaponType[rand];
    }
}

[System.Serializable]
public class WeaponClassInfo
{
    public WeaponClass weaponClass;
    public int price;
}
