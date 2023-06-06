using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioData
{
    public AudioMixerGroup mixerGroup;
    public AudioClip clip;
}

public class Sound2D: MonoBehaviour
{
    public static Sound2D Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    [SerializeField] AudioData uiButtonAudio;
    private AudioSource source;
    private AudioSource oneShotSource;

    private void Start()
    {
        AudioSource[] foundSources = GetComponents<AudioSource>();
        if(foundSources.Length == 2)
        {
            source = foundSources[0];
            oneShotSource = foundSources[1];
        }
        else
        {
            Debug.LogError("Enough Audio Sources are not present on the object, must have 2 audio sources");
        }
    }

    public void PlayButtonAudio()
    {
        PlayOneShotAudio(uiButtonAudio);
    }

    public void PlayAudio(AudioData audioData)
    {
        source.outputAudioMixerGroup = audioData.mixerGroup;
        PlayAudio(audioData.clip);
    }

    public void PlayAudio(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void PlayOneShotAudio(AudioData audioData)
    {
        oneShotSource.outputAudioMixerGroup = audioData.mixerGroup;
        oneShotSource.PlayOneShot(audioData.clip);
    }

    public void PlayOneShotAudio(AudioClip clip)
    {
        oneShotSource.PlayOneShot(clip);
    }
}
