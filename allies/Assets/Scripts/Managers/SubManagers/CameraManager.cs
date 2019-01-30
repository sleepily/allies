using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SubManager
{
  public Animator animator;
  public SpriteRenderer fadeSpriteRenderer;

  private void Start()
  {
    if (!animator)
      animator = GetComponentInChildren<Animator>();

    if (!fadeSpriteRenderer)
      fadeSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
  }

  public void Default()
  {
    animator.SetTrigger("default");
  }

  public void FadeIn(Color fadeColor)
  {
    fadeSpriteRenderer.color = fadeColor;
    animator.SetTrigger("fadeIn");
  }

  public void FadeOut(Color fadeColor)
  {
    fadeSpriteRenderer.color = fadeColor;
    animator.SetTrigger("fadeOut");
  }
}