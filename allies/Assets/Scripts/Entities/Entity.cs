using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  public GameManager gameManager;

  public virtual void MoveToInteractablesManager()
  {
    transform.SetParent(gameManager.interactablesManager.transform);
  }
}
