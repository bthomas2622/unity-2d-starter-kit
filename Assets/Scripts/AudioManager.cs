using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource[] audioSources;
    private AudioSource musicPlayer;
    private List<AudioSource> effectsPlayers = new List<AudioSource>();
    private int curEffectPlayer = 0;
    private int numEffectPlayers;
    private float lowPitchRange = .95f;
    private float highPitchRange = 1.05f;
    public AudioClip MenuMove;
    public AudioClip MenuSelect;
    public AudioClip MenuBack;
    public AudioClip MenuMusic;

    public void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSources = gameObject.GetComponents<AudioSource>();
        int numAudioSources = audioSources.Length;
        numEffectPlayers = numAudioSources - 1;
        for (int i = 0; i < numAudioSources; i++)
        {
            if (i == 0)
            {
                musicPlayer = audioSources[i];
            }
            else
            {
                effectsPlayers.Add(audioSources[i]);
            }
        }
        UpdateEffectsVolume();
        UpdateMusicVolume();
        musicPlayer.clip = MenuMusic;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }

    public void PlayMenuMove()
    {
        PlaySingle(MenuMove);
    }

    public void PlayMenuSelect()
    {
        PlaySingle(MenuSelect);
    }

    public void PlayMenuBack()
    {
        PlaySingle(MenuBack);
    }

    public void UpdateMusicVolume()
    {
        musicPlayer.volume = PlayerSettings.Instance.GetMusicVolume() / 10f; 
    }

    public void UpdateEffectsVolume()
    {
        foreach (AudioSource audioSource in effectsPlayers)
        {
            audioSource.volume = PlayerSettings.Instance.GetEffectsVolume() / 10f;
        }
    }

    private void setEffectPlayer()
    {
        curEffectPlayer += 1;
        if (curEffectPlayer >= numEffectPlayers - 1)
        {
            curEffectPlayer = 0;
        }
    }

    public void PlaySingle(AudioClip clip)
    {
        setEffectPlayer();
        effectsPlayers[curEffectPlayer].clip = clip;
        effectsPlayers[curEffectPlayer].pitch = 1.0f;
        effectsPlayers[curEffectPlayer].Play();
    }

    public void PlaySingleRandomPitch(AudioClip clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        setEffectPlayer();
        effectsPlayers[curEffectPlayer].pitch = randomPitch;
        effectsPlayers[curEffectPlayer].clip = clip;
        effectsPlayers[curEffectPlayer].Play();
    }
}
