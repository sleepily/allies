﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpikes : Spikes
{
  Animator animator;
  public SpriteRenderer spikes, eyes;

  public override void Init()
  {
    base.Init();

    animator = GetComponent<Animator>();
    spikes = GetComponent<SpriteRenderer>();
    eyes = GetComponentInChildren<SpriteRenderer>();
  }

  protected override void CheckForCharacterCollision(Collision2D collision)
  {
    if (actionActivated)
      return;

    base.CheckForCharacterCollision(collision);
  }

  public override void Activate()
  {
    base.Activate();
    spikes.color = new Color(1, 1, 1, 0);
    polygonCollider2D.isTrigger = true;
  }
}
