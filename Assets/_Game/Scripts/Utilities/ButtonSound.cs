using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayAudio()
    {
        SoundManager.Ins.PlayAudio(AudioType.ButtonClick);
    }
}
