using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SubManager
{
  public enum MusicSelector
  {
    none,
    menu,
    credits,
    levelSelection,
    level
  }

  public enum MixerSelector
  {
    master,
    music,
    effects
  }

  public MusicSelector musicSelector;
  public float musicTransitionSpeed = 3f;

  [Header("Mixer")]
  public AudioMixer audioMixer;
  public AudioMixerGroup mixer_master, mixer_characters, mixer_interactables;

  [SerializeField]
  public string[] audioMixerNames = new string[3];
  public float[] audioMixerVolume = new float[3];

  [Header("Character Sounds")]
  public AudioClip character_jump;
  public AudioClip character_land;
  public AudioClip character_meet;

  [Header("Level Sounds")]
  public AudioClip level_fail;
  public AudioClip level_retry;
  public AudioClip level_finish;

  [Header("Menu Sounds")]
  public AudioClip menu_confirm;
  public AudioClip menu_back;

  [Header("Audio Sources")]
  public AudioSource uiAudioSource;

  [Header("Music Sources")]
  public AudioSource music_always;
  public AudioSource music_credits;
  public AudioSource music_level;
  public AudioSource music_selection;

  List<AudioSource> musicSources = new List<AudioSource>();

  public override void Init()
  {
    base.Init();
  }

  private void Start()
  {
    ApplyMixerVolumeSettings();
    InitMusic();
  }

  void ApplyMixerVolumeSettings()
  {
    for (int mixerIndex = 0; mixerIndex < audioMixerVolume.Length; mixerIndex++)
      audioMixer.SetFloat("Volume_" + audioMixerNames[mixerIndex], audioMixerVolume[mixerIndex]);
  }

  void InitMusic()
  {
    musicSources.Add(music_always);
    musicSources.Add(music_credits);
    musicSources.Add(music_selection);
    musicSources.Add(music_level);

    FadeToCurrentMusic();
  }

  public void SetMixerVolume(MixerSelector destination, float volume)
  {
    int mixerIndex = (int)destination;

    audioMixerVolume[mixerIndex] = volume;
    audioMixer.SetFloat("Volume_" + audioMixerNames[mixerIndex], volume);
  }

  private void Update()
  {
    FadeToCurrentMusic();
  }

  public static void PlayOneShot(AudioClip audio, AudioSource audioSource)
  {
    audioSource.loop = false;
    Play(audio, audioSource);
  }

  public static void PlayLoop(AudioClip audio, AudioSource audioSource)
  {
    audioSource.loop = false;
    Play(audio, audioSource);
  }

  static void Play(AudioClip audio, AudioSource audioSource)
  {
    if (!audio || !audioSource)
      return;
    
    audioSource.PlayOneShot(audio);
  }

  public static void Stop(AudioSource audioSource)
  {
    audioSource.Stop();
  }

  void FadeToCurrentMusic()
  {
    float[] volumeOverrides = new float[musicSources.Count];

    switch (musicSelector)
    {
      case MusicSelector.none:
        break;
      case MusicSelector.menu:
        volumeOverrides[0] = 1;
        break;
      case MusicSelector.credits:
        volumeOverrides[0] = 1;
        volumeOverrides[1] = 1;
        break;
      case MusicSelector.levelSelection:
        volumeOverrides[0] = 1;
        volumeOverrides[2] = 1;
        break;
      case MusicSelector.level:
        volumeOverrides[0] = 1;
        volumeOverrides[2] = 1;
        volumeOverrides[3] = 1;
        break;
    }

    for (int sourceIndex = 0; sourceIndex < musicSources.Count; sourceIndex++)
    {
      AudioSource musicSource = musicSources[sourceIndex];
      musicSource.volume = Mathf.Lerp(musicSource.volume, volumeOverrides[sourceIndex], Time.deltaTime * musicTransitionSpeed);
    }
  }
}
