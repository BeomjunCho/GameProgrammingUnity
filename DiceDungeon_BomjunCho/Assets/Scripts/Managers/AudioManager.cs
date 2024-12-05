using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// AudioManager is a singleton class responsible for managing and playing various types of audio in the game.
/// It provides methods for playing 3D sound effects, standard sound effects, music, and ambience tracks.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer audioMixer;
    public AudioSource music; // The audio source for music playback.
    public AudioSource ambience; // The audio source for ambience playback.
    public AudioSource sfx; // The audio source for general sound effects playback.
    public AudioClip[] musicList; // A list of available music tracks.
    public AudioClip[] sfxList;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider ambSlider;


    private float originalMusicVolume; // To store the original music volume.

    /// <summary>
    /// Plays a 3D sound effect at a given position in the world.
    /// The sound is spatialized and attached to a specified transform.
    /// </summary>
    /// <param name="SfxName">The name of the sound effect.</param>
    /// <param name="clip">The audio clip to play.</param>
    /// <param name="spawntransform">The transform that the sound will be attached to.</param>
    /// <param name="volume">The volume of the sound effect.</param>
    public void Play3dSfx(string SfxName, AudioClip clip, Transform spawntransform, float volume)
    {
        GameObject go = new GameObject(SfxName + "Sound");
        go.transform.parent = spawntransform;
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SfxVolume")[0];
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1.0f; // Ensures it's fully spatialized
        audioSource.Play();

        Destroy(go, clip.length);
    }

    /// <summary>
    /// Plays a standard sound effect without spatialization and stops it after it finishes.
    /// </summary>
    /// <param name="SfxName">The name of the sound effect.</param>
    /// <param name="clip">The audio clip to play.</param>
    /// <param name="volume">The volume of the sound effect.</param>
    public void PlaySfx(AudioClip clip, float volume)
    {

        // Check if an SFX is already playing
        if (sfx.isPlaying)
        {
            return; // if sfx is playing, skip new sfx
        }

        // Store the original music volume and reduce it
        originalMusicVolume = music.volume;
        music.volume = Mathf.Max(0, music.volume - 0.1f);

        sfx.clip = clip;
        sfx.volume = volume;
        sfx.Play();

        // Restore music volume after SFX duration
        StartCoroutine(StopSfxAndRestoreMusicVolume(clip.length));
    }

    /// <summary>
    /// Coroutine to restore the music volume after the SFX finishes.
    /// </summary>
    private IEnumerator StopSfxAndRestoreMusicVolume(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (sfx.isPlaying)
        {
            sfx.Stop();
        }
        // Restore the original music volume
        music.volume = originalMusicVolume;
    }

    /// <summary>
    /// Plays a music track, looping it continuously.
    /// </summary>
    /// <param name="audioClip">The music track to play.</param>
    /// <param name="volume">The volume of the music track.</param>
    public void PlayMusic(AudioClip audioClip, float volume)
    {
        music.clip = audioClip;
        music.loop = true;
        music.volume = volume;
        music.Play();
    }

    /// <summary>
    /// Stops the currently playing music track.
    /// </summary>
    public void StopMusic()
    {
        if (music.isPlaying)
        {
            music.Stop();
        }
    }

    /// <summary>
    /// Plays an ambience track, looping it continuously.
    /// </summary>
    /// <param name="audioClip">The ambience track to play.</param>
    /// <param name="volume">The volume of the ambience track.</param>
    public void PlayAmbience(AudioClip audioClip, float volume)
    {
        ambience.clip = audioClip;
        ambience.loop = true;
        ambience.volume = volume;
        ambience.Play();
    }

    /// <summary>
    /// Stops the currently playing ambience track.
    /// </summary>
    public void StopAmbience()
    {
        if (ambience.isPlaying)
        {
            ambience.Stop();
        }
    }
    public void MusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SfxVolume()
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(sfxSlider.value) * 20);
    }

    public void AmbVolume()
    {
        audioMixer.SetFloat("AmbVolume", Mathf.Log10(ambSlider.value) * 20);
    }

}
