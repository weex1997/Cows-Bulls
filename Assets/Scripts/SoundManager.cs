using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //sound valuom
    public Slider effectVolum;
    public Slider musicVolume;

    //effect icon
    public GameObject effectIconMute;

    //Music icon
    public GameObject musicIconMute;

    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;

    // Singleton instance.
    public static SoundManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        EffectsSource.volume = effectVolum.value;
        MusicSource.volume = musicVolume.value;

    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        EffectsSource.pitch = randomPitch;
        EffectsSource.PlayOneShot(clips[randomIndex], 0.7F);
    }

    public void ToggleMusic()
    {

        MusicSource.mute = !MusicSource.mute;

        if (MusicSource.mute == true)
        {
            musicIconMute.SetActive(true);
        }
        else
        {
            musicIconMute.SetActive(false);
        }
    }
    public void ToggleSFX()
    {

        EffectsSource.mute = !EffectsSource.mute;
        if (EffectsSource.mute == true)
        {
            effectIconMute.SetActive(true);
        }
        else
        {
            effectIconMute.SetActive(false);
        }

    }

    public void MusicVolume()
    {

        MusicSource.volume = musicVolume.value;
    }

    public void SFXVolume()
    {

        EffectsSource.volume = effectVolum.value;

    }
}
