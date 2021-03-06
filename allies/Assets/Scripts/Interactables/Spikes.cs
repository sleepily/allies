﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Interactable
{
  bool killed = false;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckForCharacterCollision(collision);
  }

  protected virtual void CheckForCharacterCollision(Collision2D collision)
  {
    if (actionActivated)
      return;

    if (!collision.gameObject.CompareTag("Character"))
      return;

    //dont hurt anxiety if his ability is active
    Anxiety anxiety = collision.gameObject.GetComponent<Anxiety>();
    Stress stress = collision.gameObject.GetComponent<Stress>();

    if (anxiety)
      if (anxiety.abilityActive)
        return;

    if (stress)
      if (stress.abilityActive)
        return;

    KillCharacter();
  }

  public override void Activate()
  {
    base.Activate();
  }

  void KillCharacter()
  {
    if (killed)
      return;

    gameManager.sceneManager.RetryLevelOnKill();

    killed = true;
  }
}
