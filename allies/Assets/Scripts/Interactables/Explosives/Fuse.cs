using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : Interactable
{
  public FuseFlame fuseflame;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckForRampageCollision(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckForRampageCollision(collision);
  }

  void CheckForRampageCollision(Collider2D collision)
  {
    Rage rage = collision.gameObject.GetComponent<Rage>();

    if (!rage)
      return;

    if (!rage.abilityActive)
      return;

    Activate();
  }

  public override void Activate()
  {
    ActivateFuseFlame();
    base.Activate();
  }

  void ActivateFuseFlame()
  {
    if (actionActivated)
      return;

    if (!fuseflame)
      return;

    fuseflame.Activate();
  }
}
