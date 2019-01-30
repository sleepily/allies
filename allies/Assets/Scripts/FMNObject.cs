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

  public virtual void Init()
  {
    gameManager = GameManager.globalGameManager;
    initialized = true;
  }

  public virtual void MoveToParentTransform()
  {
    // don't move anywhere if object is not specialized
    return;
  }
}
