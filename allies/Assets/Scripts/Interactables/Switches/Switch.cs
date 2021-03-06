﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
  [Header("Visuals")]
  public Sprite spriteActivated;
  public Sprite spriteDeactivated;
  protected SpriteRenderer spriteRenderer;

  [Header("Activation")]
  public List<Interactable> listToTrigger;
  public bool isActivated = false;

  public override void Init()
  {
    base.Init();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    ActivateSwitchOnTagCollision("IceTear", collision);
  }

  protected void ActivateSwitchOnTagTrigger(string tag, Collider2D collision)
  {
    if (!collision.gameObject.CompareTag(tag))
      return;

    ActivateSwitch();
  }

  protected virtual void ActivateSwitchOnTagCollision(string tag, Collision2D collision)
  {
    if (!collision.gameObject.CompareTag(tag))
      return;

    ActivateSwitch();
  }

  protected virtual void ActivateSwitch()
  {
    if (isActivated)
      return;

    isActivated = true;
    spriteRenderer.sprite = spriteActivated;

    foreach (Interactable interactibleToTrigger in listToTrigger)
      if (interactibleToTrigger)
        interactibleToTrigger.Activate();
  }

  protected virtual void DeactivateSwitch()
  {
    isActivated = false;
    spriteRenderer.sprite = spriteDeactivated;

    foreach (Interactable interactibleToTrigger in listToTrigger)
      if (interactibleToTrigger)
        interactibleToTrigger.Deactivate();
  }
}
