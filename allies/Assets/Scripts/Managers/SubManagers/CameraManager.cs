using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SubManager
{
  public Animator animator;

  private void Start()
  {
    if (!animator)
      animator = GetComponentInChildren<Animator>();
  }

  public void Default()
  {
    animator.SetTrigger("default");
  }

  public void FadeIn()
  {
    animator.SetTrigger("fadeIn");
  }

  public void FadeOut()
  {
    animator.SetTrigger("fadeOut");
  }
}