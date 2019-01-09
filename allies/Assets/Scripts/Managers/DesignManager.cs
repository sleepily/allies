using System.Collections.Generic;
using UnityEngine;

public class DesignManager : MonoBehaviour
{
  public GameManager gameManager;
  
  public List<CharacterPlaceholder> characterPlaceholders;

  public List<Interactable> interactibles;

  private void Start()
  {
    foreach (Interactable interactible in interactibles)
    {
      if (interactible == null)
        continue;

      interactible.gameManager = gameManager;
      interactible.MoveToInteractiblesManager();
    }

    gameObject.SetActive(false);
    enabled = false;
  }
}