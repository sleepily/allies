using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : SubManager
{
  public override void Init()
  {
    base.Init();

    InitializeAllInteractables();
  }

  void InitializeAllInteractables()
  {
    foreach (Interactable interactable in FindObjectsOfType<Interactable>())
      interactable.Init();
    foreach (Enemy enemy in FindObjectsOfType<Enemy>())
      enemy.Init();
  }
}
