using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Interactible
{
  public Animator animator;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckForRageColliding(collision);
  }

  void CheckForRageColliding(Collision2D collision)
  {
    if (collision.gameObject.name != "Rage")
      return;

    if (gameManager.playerManager.rage.abilityActive)
      Destroy(gameObject);
  }
}
