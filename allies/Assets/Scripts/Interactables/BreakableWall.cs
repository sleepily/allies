using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Interactable
{
  public Animator animator;
  public PhysicsMaterial2D physicsMaterial2D;

  public override void Init()
  {
    base.Init();
    polygonCollider2D.sharedMaterial = physicsMaterial2D;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckForRageColliding(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CheckForRageColliding(collision);
  }

  void CheckForRageColliding(Collision2D collision)
  {
    Rage rage = collision.gameObject.GetComponent<Rage>();

    if (!rage)
      return;

    if (rage.abilityActive)
      Destroy(gameObject);
  }
}
