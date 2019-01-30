using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  public GameManager gameManager;

  private void Start()
  {
    Init();
  }

  public virtual void Init()
  {
    gameManager = GameManager.globalGameManager;
  }

  public virtual void MoveToInteractablesManager()
  {
    transform.SetParent(gameManager.interactablesManager.transform);
  }
}
