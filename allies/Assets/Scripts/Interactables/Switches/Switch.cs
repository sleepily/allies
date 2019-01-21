using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
  public List<Interactable> listToTrigger;
  public bool isActivated = false;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    ActivateSwitchOnTagCollision("IceTear", collision);
  }

  protected virtual void ActivateSwitchOnTagCollision(string tag, Collision2D collision)
  {
    if (!collision.gameObject.CompareTag(tag))
      return;

    ActivateSwitch();
  }

  protected virtual void ActivateSwitch()
  {
    isActivated = true;
    foreach (Interactable i in listToTrigger)
      i.Activate();
  }

  protected virtual void DeactivateSwitch()
  {
    isActivated = false;
    foreach (Interactable i in listToTrigger)
      i.Deactivate();
  }
}
