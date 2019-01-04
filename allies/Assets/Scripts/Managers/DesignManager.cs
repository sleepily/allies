using System.Collections.Generic;
using UnityEngine;

public class DesignManager : MonoBehaviour
{
  public GameManager gameManager;

  [Header("Interactibles/Entities to be added")]
  public GameObject ragPlaceholder;
  public GameObject anxPlaceholder;
  public GameObject depPlaceholder;

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