using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : Interactable
{
  public PhysicsMaterial2D icePhysicsMaterial;

  public override void Init()
  {
    base.Init();
    rb.sharedMaterial = icePhysicsMaterial;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    MakeCharacterSlide(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    MakeCharacterSlide(collision);
  }

  //TODO: finish this
  void MakeCharacterSlide(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    var rb = collision.gameObject.GetComponent<Rigidbody2D>();

    Vector2 slideVelocity = rb.velocity;
    slideVelocity.x = -slideVelocity.x;
    slideVelocity.y = -Mathf.Abs(slideVelocity.y);

    rb.velocity = slideVelocity;
  }
}
