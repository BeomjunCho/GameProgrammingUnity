using System.Collections;
using UnityEngine;

/// <summary>
/// AudioManager is a singleton class responsible for managing and playing various types of audio in the game.
/// It provides methods for playing 3D sound effects, standard sound effects, music, and ambience tracks.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    public AudioSource music; // The audio source for music playback.
    public AudioSource ambience; // The audio source for ambience playback.
    public AudioSource sfx; // The audio source for general sound effects playback.
    public AudioClip[] musicList; // A list of available music tracks.
    public AudioClip[] sfxList;

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
        audioSource.clip = clip;
        audioSource.volume = volume;
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
        sfx.clip = clip;
        sfx.volume = volume;
        sfx.Play();
        StartCoroutine(StopSfxAfterDuration(clip.length));
    }

    /// <summary>
    /// Stops the sound effect after its duration.
    /// </summary>
    /// <param name="duration">The length of the sound effect clip.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator StopSfxAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (sfx.isPlaying)
        {
            sfx.Stop();
        }
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
}
