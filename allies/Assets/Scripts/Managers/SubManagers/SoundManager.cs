using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SubManager
{
  public enum MusicSelector
  {
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
  [SerializeField]
  public string[] audioMixerNames = new string[3];
  public float[] audioMixerVolume = new float[3];

  [Header("Character Sounds")]
  public AudioClip character_jump;
  public AudioClip character_land;

  [Header("Level Sounds")]
  public AudioClip level_fail;
  public AudioClip level_retry;
  public AudioClip level_finish;

  [Header("Menu Sounds")]
  public AudioClip menu_confirm;
  public AudioClip menu_back;

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

  public static void PlayOneShot(AudioClip audio, FMNObject obj)
  {
    obj.audioSource.PlayOneShot(audio);
  }

  void FadeToCurrentMusic()
  {
    float[] volumes = new float[musicSources.Count];

    switch (musicSelector)
    {
      case MusicSelector.menu:
        volumes[0] = 1;
        break;
      case MusicSelector.credits:
        volumes[0] = 1;
        volumes[1] = 1;
        break;
      case MusicSelector.levelSelection:
        volumes[0] = 1;
        volumes[2] = 1;
        break;
      case MusicSelector.level:
        volumes[0] = 1;
        volumes[2] = 1;
        volumes[3] = 1;
        break;
    }

    for (int sourceIndex = 0; sourceIndex < musicSources.Count; sourceIndex++)
    {
      AudioSource musicSource = (AudioSource)musicSources[sourceIndex];
      musicSource.volume = Mathf.Lerp(musicSource.volume, volumes[sourceIndex], Time.deltaTime * musicTransitionSpeed);
    }
  }
}
