using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMushroom : Interactable
{
  public List<Interactable> interactablesToExplode;

  public override void Activate()
  {
    base.Activate();

    foreach (Interactable interactable in interactablesToExplode)
    {
      if (!interactable)
        return;

      Destroy(interactable.gameObject);
    }

    Destroy(this.gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collision)
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
    gameManager.sceneManager.RetryLevelOnKill();
  }
}
