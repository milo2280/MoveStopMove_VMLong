using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public Player player;

    public string playerName;
    public int gold;
    public int totalScore;
    public int currentLevel;
    public WeaponType weaponType;
    public List<WeaponType> unlockedWeapon = new List<WeaponType>();
    public List<WeaponClass> unlockedClass = new List<WeaponClass>();

    public List<SkinType> equippedSkin = new List<SkinType>();

    private const string NAME = "name";
    private const string GOLD = "gold";
    private const string SCORE = "score";
    private const string LEVEL = "level";

    private const string WEAPON = "weapon";
    private const string WEAPON_COUNT = "weapon_count";

    private const string CLASS = "class";
    private const string CLASS_COUNT = "class_count";

    private const string SKIN = "skin";
    private const string SKIN_COUNT = "skin_count";

    private const string DEFAULT_NAME = "You";

    public void LoadData()
    {
        weaponType = PlayerPrefs.GetInt(WEAPON) == 0 ? WeaponType.Hammer1 : (WeaponType)PlayerPrefs.GetInt(WEAPON);
        playerName = string.IsNullOrEmpty(PlayerPrefs.GetString(NAME)) ? DEFAULT_NAME : PlayerPrefs.GetString(NAME);
        currentLevel = PlayerPrefs.GetInt(LEVEL) == 0 ? 1 : PlayerPrefs.GetInt(LEVEL);
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
                equippedSkin.Add((SkinType)PlayerPrefs.GetInt(SKIN + i));
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

        PlayerPrefs.SetInt(SKIN_COUNT, equippedSkin.Count);
        for (int i = 0; i < equippedSkin.Count; i++)
        {
            PlayerPrefs.SetInt(SKIN + i, (int)equippedSkin[i]);
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

    public void UnlockSkin(SkinType skinType)
    {
        equippedSkin.Add(skinType);
    }
}
