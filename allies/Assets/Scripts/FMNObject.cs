using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Forget Me Not Object
 * 
 * Base class for every entity and interactable in the game.
 */

public class FMNObject : MonoBehaviour
{
  [HideInInspector]
  public GameManager gameManager;

  [HideInInspector]
  public bool initialized = false;

  private void Awake()
  {
    gameManager = GameManager.globalGameManager;
  }

  public virtual void Init()
  {
    gameManager = GameManager.globalGameManager;
    MoveToParentTransform();
    initialized = true;
  }

  public virtual void MoveToParentTransform()
  {
    // don't move anywhere if object is not specialized
    return;
  }

  protected virtual void SetAnimatorTrigger(Animator animator, string trigger)
  {
    animator.SetTrigger(trigger);
  }

  protected virtual void SetAnimatorTrigger(Animator animator, string trigger, bool reset = false)
  {
    if (!reset)
      animator.SetTrigger(trigger);
    else
      animator.ResetTrigger(trigger);
  }
}
