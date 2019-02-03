using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : Interactable
{
  public FireFlowerFlame flame;
  public bool startActivated = false;

  public override void Init()
  {
    gameManager = GameManager.globalGameManager;

    MoveToParentTransform();

    flame.Init();

    if (startActivated)
      Activate();

    initialized = true;
  }

  public override void Activate()
  {
    base.Activate();
    flame.Activate();
  }

  public override void Deactivate()
  {
    base.Deactivate();
    flame.Deactivate();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    KillCharacterOnTrigger(collision);
    DeactivateOnCrybabyTrigger(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    KillCharacterOnTrigger(collision);
    DeactivateOnCrybabyTrigger(collision);
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    ActivateOnRampageTrigger(collision);
  }

  public void ActivateOnRampageTrigger(Collider2D collision)
  {
    if (actionActivated)
      return;

    if (!collision.gameObject.CompareTag("Character"))
      return;

    Rage rage = collision.gameObject.GetComponent<Rage>();

    if (!rage)
      return;

    if (rage.abilityActive)
      Activate();
  }

  public void DeactivateOnCrybabyTrigger(Collider2D collision)
  {
    if (!actionActivated)
      return;

    if (!collision.gameObject.CompareTag("CrybabyTear"))
      return;

    Deactivate();
  }

  public void KillCharacterOnTrigger(Collider2D collision)
  {
    if (!actionActivated)
      return;

    if (!collision.gameObject.CompareTag("Character"))
      return;

    Rage rage = collision.gameObject.GetComponent<Rage>();

    if (rage)
      if (rage.abilityActive)
        if (!actionActivated)
          return;

    gameManager.sceneManager.RetryLevelOnKill();
  }
}
