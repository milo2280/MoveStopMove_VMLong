using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOption : MonoBehaviour
{
    public Transform weaponParentTF;
    public GameObject lockIcon;

    private WeaponItem currentWeapon;
    private Dictionary<WeaponType, WeaponItem> dictShowedWeapon = new Dictionary<WeaponType, WeaponItem>();

    public void ShowIcon(WeaponType weaponType)
    {
        lockIcon.SetActive(!PlayerData.Ins.unlockedWeapon.Contains(weaponType));

        if (currentWeapon != null)
        {
            if (currentWeapon.weaponType == weaponType) return;
            currentWeapon.gameObject.SetActive(false);
        }

        if (dictShowedWeapon.ContainsKey(weaponType))
        {
            currentWeapon = dictShowedWeapon[weaponType];
        }
        else
        {
            currentWeapon = Instantiate(WeaponManager.Ins.dictWeaponItem[weaponType], weaponParentTF);
            dictShowedWeapon.Add(currentWeapon.weaponType, currentWeapon);
        }

        currentWeapon.gameObject.SetActive(true);
    }
}
