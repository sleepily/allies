using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SubManager
{
  public static string CharacterJump = "event:/sfx/entities/characters/character jump";
  public static string CharacterLand = "event:/sfx/entities/characters/character land";
  public static string CharacterDie = "event:/sfx/entities/characters/character die";
  
  public static void PlaySound(string path, FMNObject obj)
  {
    FMODUnity.RuntimeManager.PlayOneShot(path, obj.transform.position);
  }

  public static void PlaySoundAttached(string path, FMNObject obj)
  {
    FMODUnity.RuntimeManager.PlayOneShotAttached(path, obj.gameObject);
  }
}
