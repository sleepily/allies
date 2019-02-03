using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerFlame : Interactable
{
  public bool startActivated = false;

  Animator animator;

  public override void Init()
  {
    gameManager = GameManager.globalGameManager;
    MoveToParentTransform();

    animator = GetComponent<Animator>();

    if (startActivated)
      Activate();
  }

  public override void Activate()
  {
    if (actionActivated)
      return;

    base.Activate();

    SetAnimatorTrigger(animator, "activate");
  }

  public override void Deactivate()
  {
    if (!actionActivated)
      return;

    base.Deactivate();

    SetAnimatorTrigger(animator, "deactivate");
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckFuseCollision(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckFuseCollision(collision);
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
