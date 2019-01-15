using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class SpriteFade : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  
  public float timeBeforeFade = 1f;
  public float fadeInTime = 1f;
  public float holdTime = 1f;
  public float fadeOutTime = 1f;
  public float holdTimeAfter = 1f;
  public bool isFinished = false;

  float timestamp_start;
  float timestamp_fadeIn;
  float timestamp_hold1;
  float timestamp_fadeOut;
  float timestamp_hold2;
  float timestamp_finished;

  void Start ()
  {
    CalculateTimestamps();
	}
	
	void Update ()
  {
    Fade();
	}

  void CalculateTimestamps()
  {
    timestamp_start = Time.time;
    timestamp_fadeIn = timestamp_start + timeBeforeFade;
    timestamp_hold1 = timestamp_fadeIn + fadeInTime;
    timestamp_fadeOut = timestamp_hold1 + holdTime;
    timestamp_hold2 = timestamp_fadeOut + fadeOutTime;
    timestamp_finished = timestamp_hold2 + holdTimeAfter;
  }

  void Fade()
  {
    if (isFinished)
      return;

    float opacity = CalculateOpacityValue();

    SetSpriteOpacity(opacity);
  }

  float CalculateOpacityValue()
  {
    float time = Time.time;
    float value = 0f;

    if (time < timestamp_fadeIn)
      value = 0f;

    if (time > timestamp_fadeIn)
      value = ExtensionMethods.Map01(time, timestamp_fadeIn, timestamp_hold1);

    if (time > timestamp_hold1)
      value = 1f;

    if (time > timestamp_fadeOut)
      value = 1 - ExtensionMethods.Map01(time, timestamp_fadeOut, timestamp_hold2);

    if (time > timestamp_hold2)
      value = 0f;

    if (time > timestamp_finished)
      FinishFade();

    return value;
  }

  void SetSpriteOpacity(float opacity)
  {
    Color spriteColor = spriteRenderer.color;
    spriteColor.a = opacity;
    spriteRenderer.color = spriteColor;
  }

  public void FinishFade()
  {
    isFinished = true;
    SetSpriteOpacity(0f);
  }
}
