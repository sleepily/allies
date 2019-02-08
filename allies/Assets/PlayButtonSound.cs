using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
  public enum SoundSelector
  {
    confirm,
    back
  }

  public SoundSelector sound;

  public void OnClick()
  {
    AudioSource audioSource = GameManager.globalGameManager.soundManager.uiAudioSource;
    AudioClip audioClip = null;

    if (sound == SoundSelector.confirm)
      audioClip = GameManager.globalGameManager.soundManager.menu_confirm;
    if (sound == SoundSelector.back)
      audioClip = GameManager.globalGameManager.soundManager.menu_back;

    SoundManager.PlayOneShot(audioClip, audioSource);
  }
}
