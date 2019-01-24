using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Interactable
{
  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckForCharacterCollision(collision);
  }

  protected virtual void CheckForCharacterCollision(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    //dont hurt anxiety if his ability is active
    Anxiety anxiety = collision.gameObject.GetComponent<Anxiety>();

    if (anxiety)
      if (anxiety.abilityActive)
        return;

    gameManager.levelManager.Retry();
  }
}
