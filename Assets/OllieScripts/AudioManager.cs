using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer mixer;

    [SerializeField] List<AudioSourceController> BedroomSources;
    [SerializeField] List<AudioSourceController> LibraryOneSources;
    [SerializeField] List<AudioSourceController> LibraryTwoSources;
    [SerializeField] List<AudioSourceController> CloisterSources;


    private List<AudioSourceController> audioSources;
    

    [SerializeField] List<AudioClip> footstepSoundsStone;
    [SerializeField] List<AudioClip> footstepSoundsSoil;

    public GameObject Jeremus;
    public GameObject Benedict;

    [SerializeField]
    private AudioMixerGroup musicGroup;
    [SerializeField]
    private AudioMixerGroup dialogueGroup;
    [SerializeField]
    private AudioMixerGroup ambientGroup;
    [SerializeField]
    private AudioMixerGroup ambientTwoGroup;
    [SerializeField]
    private AudioMixerGroup sfxGroup;


    private void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }*/
    }

    private void Start()
    {
        audioSources = LibraryOneSources;
        //PlayAll();
    }

    public void PlayRandomFootstep(List<AudioClip>footstepSounds)
    {
        int index = Random.Range(0, footstepSounds.Count);
        //jeremus.SetClip(footstepSounds[index]);
        //jeremusController.Play();
    }

    //just play all ready clips on this channel
    public void PlayAll()
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.IsPlaying())
            {
                audioSource.Play();
            }
        }
    }

    //specify a clip and play on this channel 
    public void PlayAll(AudioClip clip)
    {
        foreach (var audioSource in audioSources)
        {
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

    public void FadeAllSources( float duration)
    {
        foreach (var source in audioSources)
        {
            source.FadeIn(duration);
        }
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

    public void PlayDialogue(GameObject character)
    {
        AudioClip clip = character.GetComponent<CharactersDialogue>().GetNextDialogueClip();
        AudioSourceController controller = character.GetComponent<AudioSourceController>();
        controller.SetMixerGroup(dialogueGroup);
        controller.SetClip(clip);
        controller.Play();
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

    public void PlayAllAmbientTwo()
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.IsPlaying())
            {

                if (audioSource.mixerGroup != ambientTwoGroup) continue;
                audioSource.Play();
                return;
            }
        }
    }

    public void PlayAllAmbient()
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.IsPlaying())
            {
                if (audioSource.mixerGroup != ambientGroup) continue;
                audioSource.Play();
                return;
            }
        }
    }

    public void SwitchToLibraryOneAudio() => audioSources = LibraryOneSources;

    public void SwitchToLibraryTwoAudio() => audioSources = LibraryTwoSources;

    public void SwitchToCloisterAudio() => audioSources = CloisterSources;

    public void SetSFXVolume(float volume) => mixer.SetFloat(sfxGroup + "Volume", volume);
    public void SetMusicVolume(float volume) => mixer.SetFloat(musicGroup + "Volume", volume);
    public void SetDialogueVolume(float volume) => mixer.SetFloat(dialogueGroup + "Volume", volume);
    public void SetAmbientVolume(float volume) => mixer.SetFloat(ambientGroup + "Volume", volume);

    public void SetAmbientTwoVolume(float volume) => mixer.SetFloat(ambientTwoGroup + "Volume", volume);

}
