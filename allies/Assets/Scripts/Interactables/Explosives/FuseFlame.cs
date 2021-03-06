﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseFlame : Interactable
{
  Animator animator;
  SpriteRenderer flameSpriteRenderer;
  
  public override void Init()
  {
    gameManager = GameManager.globalGameManager;

    MoveToParentTransform();

    animator = GetComponent<Animator>();
    flameSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    flameSpriteRenderer.enabled = false;

    initialized = true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckExplosiveMushroomTrigger(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckExplosiveMushroomTrigger(collision);
  }
  
  void CheckExplosiveMushroomTrigger(Collider2D collision)
  {
    ExplosiveMushroom mushroom = collision.gameObject.GetComponent<ExplosiveMushroom>();

    if (!mushroom)
      return;

    ActivateMushroom(mushroom);
  }

  void ActivateMushroom(ExplosiveMushroom mushroom)
  {
    mushroom.Activate();
    Destroy(this.gameObject);
  }

  public override void Activate()
  {
    base.Activate();

    SetAnimatorTrigger(animator, "activate");
    flameSpriteRenderer.enabled = true;
  }

  public override void Deactivate()
  {
    base.Deactivate();

    SetAnimatorTrigger(animator, "deactivate");
    flameSpriteRenderer.enabled = false;
  }
}
