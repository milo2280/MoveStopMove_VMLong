using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    ThrowWeapon,
    Die,
    Countdown
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Audio[] audios;

    private bool isSoundOn;
    public bool IsSoundOn { get { return isSoundOn; } private set { } }

    private Dictionary<AudioType, AudioClip> dictAudio = new Dictionary<AudioType, AudioClip>();

    private void Awake()
    {
        isSoundOn = !audioSource.mute;

        for (int i = 0; i < audios.Length; i++)
        {
            dictAudio.Add(audios[i].soundType, audios[i].audioClip);
        }
    }

    public void PlayAudio(AudioType audioType)
    {
        audioSource.PlayOneShot(dictAudio[audioType], 0.2f);
    }

    public void ToggleSound()
    {
        audioSource.mute = !audioSource.mute;
        isSoundOn = !audioSource.mute;
    }
}

[System.Serializable]
public class Audio
{
    public AudioType soundType;
    public AudioClip audioClip;
}
