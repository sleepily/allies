using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerFlame : Interactable
{
  public FireFlower parent;
  Animator animator;

  public override void Init()
  {
    gameManager = GameManager.globalGameManager;

    MoveToParentTransform();
    InitAudioSource();

    animator = GetComponent<Animator>();
    animator.Rebind();

    if (parent.startActivated)
      Activate();
  }

  public override void MoveToParentTransform()
  {
    this.transform.SetParent(parent.transform);
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
    CheckFuseCollision(collision);
    parent.KillCharacterOnTrigger(collision);
    parent.DeactivateOnCrybabyTrigger(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckFuseCollision(collision);
    parent.KillCharacterOnTrigger(collision);
    parent.DeactivateOnCrybabyTrigger(collision);
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    parent.ActivateOnRampageTrigger(collision);
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
