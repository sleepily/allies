using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
  public List<Interactable> listToTrigger;

  public enum ActionSelector
  {
    action,
    destroy
  }

  public ActionSelector action;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckCollision(collision);
  }

  protected virtual void CheckCollision(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("IceTear"))
      return;

    TriggerSwitch();
  }

  void TriggerSwitch()
  {
    switch (action)
    {
      case ActionSelector.action:
        foreach (Interactable i in listToTrigger)
          i.Action();
        break;
      case ActionSelector.destroy:
        foreach (Interactable i in listToTrigger)
          Destroy(i.gameObject);
        break;
      default:
        break;
    }
  }
}
