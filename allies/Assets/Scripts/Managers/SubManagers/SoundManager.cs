using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SubManager
{
  public static AudioSource Jump, Land, Die;
  public static AudioSource Submit, Back, Retry;

  [SerializeField]
  public AudioSource character_jump, character_land;
  [SerializeField]
  public AudioSource level_fail, level_retry, level_finish;
  public AudioSource menu_confirm, menu_back;
  
  public static void PlaySound(AudioSource audio, FMNObject obj)
  {

  }

  public static void PlaySoundAttached(AudioSource audio, FMNObject obj)
  {

  }
}
