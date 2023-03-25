using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer mixer;

    public List<AudioSourceController> audioSources;
    public List<AudioClip> footstepSounds;
    public AudioSourceController jeremusController;

    [SerializeField]
    private AudioMixerGroup musicGroup;
    [SerializeField]
    private AudioMixerGroup dialogueGroup;
    [SerializeField]
    private AudioMixerGroup ambientGroup;
    [SerializeField]
    private AudioMixerGroup sfxGroup;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PlayAll(ambientGroup);
        PlayAll(sfxGroup);
    }

    public void PlayRandomFootstep()
    {
        int index = Random.Range(0, footstepSounds.Count);
        jeremusController.SetClip(footstepSounds[index]);
        jeremusController.Play();
    }

    //just play all ready clips on this channel
    public void PlayAll(AudioMixerGroup mixerGroup)
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.IsPlaying())
            {
                if(audioSource.mixerGroup==mixerGroup)
                    audioSource.Play();
            }
        }
    }

    //specify a clip and play on this channel 
    public void PlayAll(AudioClip clip, AudioMixerGroup mixerGroup)
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.mixerGroup != mixerGroup) continue;
            if (!audioSource.IsPlaying())
            {

                audioSource.SetClip(clip);
                
                audioSource.Play();
                return;
            }
        }
    }


    public void PlayAllAtSpecificVolume(AudioClip clip, float volume, AudioMixerGroup mixerGroup)
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.mixerGroup != mixerGroup) continue; 
            if (!audioSource.IsPlaying())
            {
                audioSource.SetClip(clip);
                audioSource.SetVolume(volume);
                audioSource.Play();
                return;
            }
        }
    }

    public void FadeMixerGroup(AudioMixerGroup mixerGroup, float targetVolume, float duration)
    {
        StartCoroutine(FadeMixerGroupCoroutine(mixerGroup, targetVolume, duration));
    }

    private IEnumerator FadeMixerGroupCoroutine(AudioMixerGroup mixerGroup, float targetVolume, float duration)
    {
        AudioMixer mixer = mixerGroup.audioMixer;
        float startVolume;
        mixer.GetFloat(mixerGroup.name + "Volume", out startVolume);
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float timeRatio = (Time.time - startTime) / duration;
            float volume = Mathf.Lerp(startVolume, targetVolume, timeRatio);
            mixer.SetFloat(mixerGroup.name + "Volume", volume);
            yield return null;
        }

        mixer.SetFloat(mixerGroup.name + "Volume", targetVolume);
    }

    public void PlayDialogue(AudioClip clip)
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.IsPlaying())
            {
                audioSource.SetClip(clip);
                audioSource.SetMixerGroup(dialogueGroup);
                audioSource.Play();
                return;
            }
        }
    }

    public void StopAll()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    public void PauseAll()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }

    public void ResumeAll()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Resume();
        }
    }

    public void SetVolumeAll(float volume)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.SetVolume(volume);
        }
    }

    public void Play(AudioClip clip, int index, AudioMixerGroup mixerGroup)
    {
        audioSources[index].SetClip(clip);
        audioSources[index].SetMixerGroup(mixerGroup);
        audioSources[index].Play();
    }

    public void Stop(int index) => audioSources[index].Stop();

    public void Pause(int index) => audioSources[index].Pause();

    public void Resume(int index) => audioSources[index].Resume();

    public void SetVolume(float volume, int index) => audioSources[index].SetVolume(volume);

    public void PlayMusic(AudioClip clip)
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.IsPlaying())
            {
                audioSource.SetClip(clip);
                audioSource.SetMixerGroup(musicGroup);
                audioSource.Play();
                return;
            }
        }
    }
}
