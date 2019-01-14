using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
  public List<Interactable> listToTrigger;

  public enum TriggerAction
  {
    action,
    destroy
  }

  public TriggerAction triggerAction;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    ActivateSwitchOnTagCollision("IceTear", collision);
  }

  protected virtual void ActivateSwitchOnTagCollision(string tag, Collision2D collision)
  {
    if (!collision.gameObject.CompareTag(tag))
      return;

    TriggerSwitch();
  }

  void TriggerSwitch()
  {
    switch (triggerAction)
    {
      case TriggerAction.action:
        foreach (Interactable i in listToTrigger)
          i.Action();
        break;
      case TriggerAction.destroy:
        foreach (Interactable i in listToTrigger)
          Destroy(i.gameObject);
        break;
      default:
        break;
    }
  }
}
