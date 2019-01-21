﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableStone : Interactable
{
  public PhysicsMaterial2D stonePhysicsMaterial;
  public PhysicsMaterial2D icePhysicsMaterial;

  public float speed = 7f;
  Rage rage;

  public override void Init()
  {
    base.Init();
    rb.sharedMaterial = stonePhysicsMaterial;
    this.rb.isKinematic = false;
    this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckForRampageCollision(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CheckForRampageCollision(collision);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    StopMoving(collision);
  }

  void CheckForRampageCollision(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    if (collision.gameObject.name != "Rage")
      return;

    if (!rage)
      rage = collision.gameObject.GetComponent<Rage>();

    if (!rage.abilityActive)
      return;

    RampageColliding();
  }

  void RampageColliding()
  {
    rb.sharedMaterial = icePhysicsMaterial;
    this.rb.mass = 100f;
  }

  void StopMoving(Collision2D collision)
  {
    if (collision.gameObject.name != "Rage")
      return;

    rb.sharedMaterial = stonePhysicsMaterial;
    this.rb.mass = 10000f;
  }
}
