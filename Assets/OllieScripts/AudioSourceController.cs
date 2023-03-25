using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceController : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public float startingVolume = 1f;
    public float startingPitch = 1f;
    public bool loop;
    public float spatialBlend;

    private AudioSource audioSource;
    public AudioClip startClip;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = startClip;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.volume = startingVolume;
        audioSource.pitch = startingPitch;
        audioSource.spatialBlend = spatialBlend;
        audioSource.loop = loop;
    }

    public IEnumerator FadeIn(float fadeDuration)
    {
        float currentTime = 0f;
        float startVolume = audioSource.volume;
        audioSource.volume = 0f;
        audioSource.Play();

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, startVolume, currentTime / fadeDuration);
            yield return null;
        }
    }

    public IEnumerator FadeOut(float fadeDuration)
    {
        float currentTime = 0f;
        float startVolume = audioSource.volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator Crossfade(AudioClip newClip, float fadeDuration)
    {
        float currentTime = 0f;
        float startVolume = audioSource.volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        audioSource.clip = newClip;
        audioSource.Play();

        currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, startVolume, currentTime / fadeDuration);
            yield return null;
        }
    }

    public void SetSpatialBlend(float blend) => audioSource.spatialBlend = blend;
    
    public void SetMute(bool isMuted) => audioSource.mute = isMuted;
    
    public void SetLooping(bool isLooping) => audioSource.loop = isLooping;
    
    public void Play()=>audioSource.Play();

    public void Stop() => audioSource.Stop();

    public void Pause() => audioSource.Pause();
    
    public void Resume() => audioSource.UnPause();

    public void SetVolume(float volume) => audioSource.volume = volume;

    public bool IsPlaying()=> audioSource.isPlaying;

    public void SetClip(AudioClip clip) => audioSource.clip = clip;

    public AudioClip GetClip() => audioSource.clip;

    public void SetMixerGroup(AudioMixerGroup mixerGroupP)
    {
        mixerGroup = mixerGroupP;
        audioSource.outputAudioMixerGroup = mixerGroupP;
    }
}
