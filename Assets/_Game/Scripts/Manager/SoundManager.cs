using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource sound;

    public AudioClip throwWeapon, die, countdown;

    public bool isSoundOn;

    private void Awake()
    {
        isSoundOn = !sound.mute;
    }

    public void PlaySound(AudioClip clip)
    {
        sound.PlayOneShot(clip, 0.2f);
    }

    public void SoundOnOff()
    {
        sound.mute = !sound.mute;
        isSoundOn = !sound.mute;
    }
}
