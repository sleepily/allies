using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumeFromSlider : MonoBehaviour
{
  public SoundManager.MixerSelector audioMixer;
  Slider slider;

  private void Start()
  {
    slider = GetComponent<Slider>();
  }

  public void SetVolume()
  {
    GameManager.globalGameManager.soundManager.SetMixerVolume(audioMixer, slider.value);
  }
}
