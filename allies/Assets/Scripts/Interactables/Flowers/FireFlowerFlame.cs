using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerFlame : Interactable
{
  public bool startActivated = false;

  Animator animator;

  public override void Init()
  {
    base.Init();
    animator = GetComponent<Animator>();

    if (startActivated)
      Activate();
  }

  public override void Activate()
  {
    base.Activate();

    SetAnimatorTrigger(animator, "activate");
  }

  public override void Deactivate()
  {
    base.Deactivate();

    SetAnimatorTrigger(animator, "deactivate");
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckCharacterCollision(collision);
    CheckFuseCollision(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckCharacterCollision(collision);
    CheckFuseCollision(collision);
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    CheckRampageCollision(collision);
  }

  void CheckRampageCollision(Collider2D collision)
  {
    if (actionActivated)
      return;

    if (!collision.gameObject.CompareTag("Rage"))
      return;

    Rage rage = collision.gameObject.GetComponent<Rage>();

    if (!rage)
      return;

    if (rage.abilityActive)
      Activate();
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

  void CheckFuseCollision(Collider2D collision)
  {
    if (!actionActivated)
      return;

    if (!collision.gameObject.CompareTag("Fuse"))
      return;

    Fuse fuse = collision.gameObject.GetComponent<Fuse>();

    if (fuse)
      fuse.Activate();
  }
}
