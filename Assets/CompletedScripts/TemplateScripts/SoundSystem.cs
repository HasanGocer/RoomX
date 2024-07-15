using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioClip mainMusic;
    [SerializeField] private List<AudioClip> effectList = new List<AudioClip>();

    public void MainMusicPlay()
    {
        mainSource.clip = mainMusic;
        mainSource.Play();
    }

    public void MainMusicStop()
    {
        mainSource.Stop();
    }

    public void EffectCall(int effectCount)
    {
        mainSource.PlayOneShot(effectList[effectCount]);
    }
}
