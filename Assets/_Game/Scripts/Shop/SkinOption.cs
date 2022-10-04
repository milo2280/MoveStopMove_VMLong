using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinOption : MonoBehaviour
{
    public GameObject equipped;
    public GameObject locked;
    public Transform skinParentTF;

    private Skin currentSkin;

    public void ShowSkin(Skin skin)
    {
        SkinData skinData = skin.data;
        if (skinData.locked != 1) locked.SetActive(false);
        else locked.SetActive(true);

        if (currentSkin != null)
        {
            SimplePool.Despawn(currentSkin);
        }

        currentSkin = SimplePool.Spawn<Skin>(skin, skinParentTF, 1);

        if (PlayerData.Ins.equippedSkins.Contains(currentSkin.skinType))
        {
            equipped.SetActive(true);
        }
        else
        {
            equipped.SetActive(false);
        }
    }
}
