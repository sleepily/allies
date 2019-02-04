using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvasGroup : MonoBehaviour
{
  public float fadeDuration = 1.2f;
  CanvasGroup canvasGroup;

  private void Start()
  {
    canvasGroup = GetComponent<CanvasGroup>();
  }

  public void FadeIn()
  {
    StartCoroutine(fadeCanvasGroup(canvasGroup, true, fadeDuration));
  }

  public void FadeOut()
  {
    StartCoroutine(fadeCanvasGroup(canvasGroup, false, fadeDuration));
  }

  public void Fade(bool isFadeIn)
  {
    StartCoroutine(fadeCanvasGroup(canvasGroup, isFadeIn, fadeDuration));
  }

  public void FadeInstant(bool isFadeIn)
  {
    StartCoroutine(fadeCanvasGroup(canvasGroup, isFadeIn, 0f));
  }

  IEnumerator fadeCanvasGroup(CanvasGroup group, bool fadeIn, float duration)
  {
    float counter = 0f;
    
    float fadeValueStart, fadeValueEnd;

    fadeValueStart = fadeIn ? 0 : 1;
    fadeValueEnd = fadeIn ? 1 : 0;
    
    while (counter <= duration)
    {
      counter += Time.deltaTime;
      group.alpha = Mathf.Lerp(fadeValueStart, fadeValueEnd, counter / duration);

      yield return null;
    }
  }
}
