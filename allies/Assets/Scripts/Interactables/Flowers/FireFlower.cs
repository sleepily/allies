using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : Interactable
{
  public FireFlowerFlame flame;

  public override void Init()
  {
    base.Init();

    if (!flame)
      flame = GetComponentInChildren<FireFlowerFlame>();

    flame.transform.SetParent(this.transform);
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
    CheckCharacterCollision(collision);
    CheckCrybabyTearCollision(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckCharacterCollision(collision);
    CheckCrybabyTearCollision(collision);
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    CheckRampageCollision(collision);
  }

  void CheckRampageCollision(Collider2D collision)
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

  void CheckCrybabyTearCollision(Collider2D collision)
  {
    if (!actionActivated)
      return;

    if (!collision.gameObject.CompareTag("CrybabyTear"))
      return;

    Deactivate();
  }

  void CheckCharacterCollision(Collider2D collision)
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
