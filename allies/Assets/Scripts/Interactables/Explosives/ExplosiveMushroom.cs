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
  }
}
