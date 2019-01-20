using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableStone : Interactable
{
  public float speed = 7f;
  Rage rage;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckForRampageCollision(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CheckForRampageCollision(collision);
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

    this.transform.position += (Vector3)rage.abilityDirection * speed * Time.deltaTime;
  }
}
