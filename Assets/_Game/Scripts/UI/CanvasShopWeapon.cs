using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasShopWeapon : UICanvas
{
    public TextMeshProUGUI weaponName, noteText, priceText, priceGrayText;
    public Transform weaponParentTF;
    public WeaponOption[] weaponOptions;
    public Button[] optionButtons;
    public Text buffText;
    public GameObject note, select, equipped, buy, buyGray, watchAds, watchAds2;

    private Dictionary<WeaponType, WeaponItem> dictShowedWeapon = new Dictionary<WeaponType, WeaponItem>();
    private WeaponItem currentWeapon;
    private WeaponClass currentClass;
    private List<WeaponType> currentList = new List<WeaponType>();
    private List<GameObject> listButtonEnabled = new List<GameObject>();

    private void OnEnable()
    {
        OnInit();
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int option = i;
            optionButtons[i].onClick.AddListener(delegate { ChooseWeapon(option); });
        }
    }

    public void OnInit()
    {
        WeaponItem playerWeapon = WeaponManager.Ins.dictWeaponItem[PlayerData.Ins.weaponType];
        currentClass = playerWeapon.weaponClass;
        UpdateShop();
    }

    private void UpdateShop()
    {
        currentList = WeaponManager.Ins.dictWeaponClass[currentClass];
        ShowWeaponName();
        ShowOption();
        ShowWeapon(0);
    }

    private void ShowWeaponName()
    {
        weaponName.text = WeaponManager.Ins.dictClassInfo[currentClass].className;
        if (!PlayerData.Ins.unlockedClass.Contains(currentClass))
        {
            weaponName.color = Color.black;
        }
    }


    private void ShowOption()
    {
        if (PlayerData.Ins.unlockedClass.Contains(currentClass))
        {
            for (int i = 0; i < currentList.Count; i++)
            {
                weaponOptions[i].gameObject.SetActive(true);
                weaponOptions[i].ShowIcon(currentList[i]);
            }

            for (int i = currentList.Count; i < weaponOptions.Length; i++)
            {
                weaponOptions[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < weaponOptions.Length; i++)
            {
                weaponOptions[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChooseWeapon(int option)
    {
        ShowWeapon(option);
    }

    private void ShowWeapon(int option)
    {
        if (currentWeapon != null)
        {
            if (currentWeapon.weaponType == currentList[option]) return;
            currentWeapon.gameObject.SetActive(false);
        }

        if (dictShowedWeapon.ContainsKey(currentList[option]))
        {
            currentWeapon = dictShowedWeapon[currentList[option]];
        }
        else
        {
            currentWeapon = Instantiate(WeaponManager.Ins.dictWeaponItem[currentList[option]], weaponParentTF);
            dictShowedWeapon.Add(currentWeapon.weaponType, currentWeapon);
        }

        currentWeapon.gameObject.SetActive(true);

        ShowBuff();
        ShowButton();
    }

    public void ShowBuff()
    {
        Buff buff = currentWeapon.buff;
        if (buff.buffClass == BuffClass.Add)
        {
            buffText.text = "+ " + buff.buffAmount.ToString() + " " + buff.buffName;
        }
    }

    private void ShowButton()
    {
        DisableAllUI();

        WeaponClass prevClass = WeaponManager.Ins.PrevClass(currentClass);

        if (PlayerData.Ins.unlockedClass.Contains(currentClass))
        {
            if (currentWeapon.weaponType == PlayerData.Ins.weaponType)
            {
                EnableUI(equipped);
            }
            else if (PlayerData.Ins.unlockedWeapon.Contains(currentWeapon.weaponType))
            {
                EnableUI(select);
            }
            else
            {
                EnableUI(watchAds2);
            }
        }
        else if (PlayerData.Ins.unlockedClass.Contains(prevClass))
        {
            EnableUI(note);
            noteText.text = "(Lock)";
            EnableUI(buy);
            priceText.text = WeaponManager.Ins.dictClassInfo[currentClass].price.ToString();
            EnableUI(watchAds);
        }
        else
        {
            EnableUI(note);
            noteText.text = "(Unlock " + WeaponManager.Ins.dictClassInfo[prevClass].className + " first)";
            EnableUI(buyGray);
            priceGrayText.text = WeaponManager.Ins.dictClassInfo[currentClass].price.ToString();
        }
    }

    private void EnableUI(GameObject obj)
    {
        obj.SetActive(true);
        listButtonEnabled.Add(obj);
    }

    private void DisableAllUI()
    {
        for (int i = 0; i < listButtonEnabled.Count; i++)
        {
            listButtonEnabled[i].SetActive(false);
        }

        listButtonEnabled.Clear();
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        LevelManager.Ins.ShowPlayer();
        Close();
    }

    public void PrevButton()
    {
        WeaponClass nextClass = WeaponManager.Ins.PrevClass(currentClass);
        if (nextClass != currentClass)
        {
            currentClass = nextClass;
            UpdateShop();
        }
    }

    public void NextButton()
    {
        WeaponClass nextClass = WeaponManager.Ins.NextClass(currentClass);
        if (nextClass != currentClass)
        {
            currentClass = nextClass;
            UpdateShop();
        }
    }

    public void SelectButton()
    {
        PlayerData.Ins.ChangeWeapon(currentWeapon.weaponType);
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        LevelManager.Ins.ShowPlayer();
        Close();
    }

    public void EquippedButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        LevelManager.Ins.ShowPlayer();
        Close();
    }

    public void BuyButton()
    {
        if (PlayerData.Ins.SpendGold(WeaponManager.Ins.dictClassInfo[currentClass].price))
        {
            PlayerData.Ins.UnlockClass(currentClass);
            ShowButton();
            ShowOption();
        }
    }

    public void WatchAdsButton()
    {
        PlayerData.Ins.UnlockClass(currentClass);
        ShowButton();
        ShowOption();
    }

    public void WatchAds2Button()
    {
        PlayerData.Ins.UnLockWeapon(currentWeapon.weaponType);
        ShowButton();
        ShowOption();
    }
}
