using UnityEngine;
using System.Collections;

public class SubManager : Manager
{
  //[HideInInspector]
  public GameManager gameManager;

  private void Start()
  {
    Init();
  }

  public virtual void Init()
  {
    gameManager = GameManager.globalGameManager;
  }
}
