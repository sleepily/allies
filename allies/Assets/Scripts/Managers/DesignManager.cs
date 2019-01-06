﻿using System.Collections.Generic;
using UnityEngine;

public class DesignManager : MonoBehaviour
{
  public GameManager gameManager;
  
  public List<CharacterPlaceholder> characterPlaceholders;

  public List<Interactible> interactibles;

  private void Start()
  {
    foreach (Interactible interactible in interactibles)
    {
      interactible.gameManager = gameManager;
      interactible.MoveToInteractiblesManager();
    }

    gameObject.SetActive(false);
    enabled = false;
  }
}