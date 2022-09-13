using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource sound, vibrate;

    public AudioClip throwWeapon, die;

    public void PlaySound(AudioClip clip)
    {
        sound.PlayOneShot(clip);
    }

    public void SoundOnOff()
    {
        sound.mute = !sound.mute;
    }
}
