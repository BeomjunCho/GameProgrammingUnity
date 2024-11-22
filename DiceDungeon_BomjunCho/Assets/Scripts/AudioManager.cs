using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource music;
    public AudioSource ambience;
    public AudioClip[] musicList;
    public void PlaySfx(string SfxName, AudioClip clip, Transform spawntransform, float volume)
    {
        GameObject go = new GameObject(SfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void PlayMusic(AudioClip audioClip, float volume)
    {
        music.clip = audioClip;
        music.loop = true;
        music.volume = volume;
        music.Play();
    }
    public void StopMusic()
    {
        if (music.isPlaying)
        {
            music.Stop();
        }
    }

    public void PlayAmbience(AudioClip audioClip, float volume)
    {
        ambience.clip = audioClip;
        ambience.loop = true;
        ambience.volume = volume;
        ambience.Play();
    }
}
