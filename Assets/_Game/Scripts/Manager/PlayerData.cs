using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public Player player;

    public string playerName;
    public int gold;
    public int totalScore;
    public int currentLevel, bestRank;
    public WeaponType weaponType;
    public List<WeaponType> unlockedWeapon = new List<WeaponType>();
    public List<WeaponClass> unlockedClass = new List<WeaponClass>();

    public List<SkinType> equippedSkins = new List<SkinType>();
    public List<SkinType> oneTimeSkins = new List<SkinType>();

    private const string NAME = "name";
    private const string GOLD = "gold";
    private const string SCORE = "score";
    private const string LEVEL = "level";
    private const string BEST = "best";

    private const string WEAPON = "weapon";
    private const string WEAPON_COUNT = "weapon_count";

    private const string CLASS = "class";
    private const string CLASS_COUNT = "class_count";

    private const string SKIN = "skin";
    private const string SKIN_COUNT = "skin_count";

    private const string ONETIME = "onetime";
    private const string ONETIME_COUNT = "onetime_count";

    private const string DEFAULT_NAME = "You";

    public void LoadData()
    {
        weaponType = PlayerPrefs.GetInt(WEAPON) == 0 ? WeaponType.Hammer1 : (WeaponType)PlayerPrefs.GetInt(WEAPON);
        playerName = string.IsNullOrEmpty(PlayerPrefs.GetString(NAME)) ? DEFAULT_NAME : PlayerPrefs.GetString(NAME);
        currentLevel = PlayerPrefs.GetInt(LEVEL) == 0 ? 1 : PlayerPrefs.GetInt(LEVEL);
        bestRank = PlayerPrefs.GetInt(BEST) == 0 ? 50 : PlayerPrefs.GetInt(BEST);
        gold = PlayerPrefs.GetInt(GOLD);
        totalScore = PlayerPrefs.GetInt(SCORE);

        if (PlayerPrefs.GetInt(WEAPON_COUNT) == 0)
        {
            PlayerPrefs.SetInt(WEAPON_COUNT, 1);
            PlayerPrefs.SetInt(WEAPON + "0", (int)WeaponType.Hammer1);
        }

        for (int i = 0; i < PlayerPrefs.GetInt(WEAPON_COUNT); i++)
        {
            unlockedWeapon.Add((WeaponType)PlayerPrefs.GetInt(WEAPON + i));
        }

        if (PlayerPrefs.GetInt(CLASS_COUNT) == 0)
        {
            PlayerPrefs.SetInt(CLASS_COUNT, 1);
            PlayerPrefs.SetInt(CLASS + "0", (int)WeaponClass.Hammer);
        }

        for (int i = 0; i < PlayerPrefs.GetInt(CLASS_COUNT); i++)
        {
            unlockedClass.Add((WeaponClass)PlayerPrefs.GetInt(CLASS + i));
        }

        if (PlayerPrefs.GetInt(SKIN_COUNT) != 0)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(SKIN_COUNT); i++)
            {
                equippedSkins.Add((SkinType)PlayerPrefs.GetInt(SKIN + i));
            }
        }

        if (PlayerPrefs.GetInt(ONETIME_COUNT) != 0)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(ONETIME_COUNT); i++)
            {
                oneTimeSkins.Add((SkinType)PlayerPrefs.GetInt(ONETIME + i));
            }
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(NAME, playerName);
        PlayerPrefs.SetInt(LEVEL, currentLevel);
        PlayerPrefs.SetInt(GOLD, gold);
        PlayerPrefs.SetInt(SCORE, totalScore);
        PlayerPrefs.SetInt(WEAPON, (int)weaponType);
        PlayerPrefs.SetInt(BEST, bestRank);

        PlayerPrefs.SetInt(WEAPON_COUNT, unlockedWeapon.Count);
        for (int i = 0; i < unlockedWeapon.Count; i++)
        {
            PlayerPrefs.SetInt(WEAPON + i, (int)unlockedWeapon[i]);
        }

        PlayerPrefs.SetInt(CLASS_COUNT, unlockedClass.Count);
        for (int i = 0; i < unlockedClass.Count; i++)
        {
            PlayerPrefs.SetInt(CLASS + i, (int)unlockedClass[i]);
        }

        PlayerPrefs.SetInt(SKIN_COUNT, equippedSkins.Count);
        for (int i = 0; i < equippedSkins.Count; i++)
        {
            PlayerPrefs.SetInt(SKIN + i, (int)equippedSkins[i]);
        }

        PlayerPrefs.SetInt(ONETIME_COUNT, oneTimeSkins.Count);
        for (int i = 0; i < oneTimeSkins.Count; i++)
        {
            PlayerPrefs.SetInt(ONETIME + i, (int)oneTimeSkins[i]);
        }
    }

    public void SetPlayerName(string name)
    {
        this.playerName = name;
        player.SetName(name);
    }

    public void ReceiveGold(int gold)
    {
        this.gold += (int)(gold * player.gold);
    }

    public bool SpendGold(int gold)
    {
        if (this.gold >= gold)
        {
            this.gold -= gold;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddTotalScore(int score)
    {
        this.totalScore += score;
    }

    public void ChangeWeapon(WeaponType newType)
    {
        weaponType = newType;
    }

    public void UnLockWeapon(WeaponType type)
    {
        unlockedWeapon.Add(type);
    }

    public void UnlockClass(WeaponClass weaponClass)
    {
        unlockedClass.Add(weaponClass);
        unlockedWeapon.Add(WeaponManager.Ins.dictWeaponClass[weaponClass][0]);
    }

    public void EquipSkin(SkinType skinType)
    {
        SkinClass skinClass = SkinManager.Ins.GetSkinClass(skinType);

        for (int i = 0; i < equippedSkins.Count; i++)
        {
            if (skinClass == SkinManager.Ins.GetSkinClass(equippedSkins[i]))
            {
                equippedSkins.RemoveAt(i);
                break;
            }
        }

        equippedSkins.Add(skinType);
    }

    public void UnequipSkin(SkinType skinType)
    {
        if (equippedSkins.Contains(skinType))
        {
            equippedSkins.Remove(skinType);
        }
    }

    public void UnlockOneTime(SkinType skinType)
    {
        oneTimeSkins.Add(skinType);
    }

    public void ResetOneTime()
    {
        for (int i = 0; i < oneTimeSkins.Count; i++)
        {
            Skin skin = SkinManager.Ins.dictSkin[oneTimeSkins[i]];
            skin.data.locked = 1;
            UnequipSkin(oneTimeSkins[i]);
        }

        oneTimeSkins.Clear();
    }
}
