using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SubManager
{
  public Animator animator;
  public SpriteRenderer fadeSpriteRenderer;

  public Color backgroundColor = new Color(0.1019608f, 0.1058824f, 0.1490196f);

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
    animator.ResetTrigger("fadeIn");
    animator.ResetTrigger("fadeOut");
  }

  public void FadeIn(Color fadeColor)
  {
    fadeSpriteRenderer.color = fadeColor;
    animator.SetTrigger("fadeIn");
    animator.ResetTrigger("fadeOut");
  }

  public void FadeOut(Color fadeColor)
  {
    fadeSpriteRenderer.color = fadeColor;
    animator.SetTrigger("fadeOut");
    animator.ResetTrigger("fadeIn");
  }

  public void ResetCameraPosition()
  {
    GameManager.globalCamera.transform.position = Vector3.forward * -20;
  }
}