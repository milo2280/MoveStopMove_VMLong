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

        if (currentSkin != null)
        {
            SimplePool.Despawn(currentSkin);
        }

        currentSkin = SimplePool.Spawn<Skin>(skin, skinParentTF, 1);
    }
}
