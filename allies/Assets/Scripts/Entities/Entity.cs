using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : FMNObject
{
  public override void MoveToParentTransform()
  {
    transform.SetParent(gameManager.interactablesManager.transform);
  }
}
