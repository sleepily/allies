using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignManager : MonoBehaviour
{
  public GameManager gameManager;

  [Header("Interactibles/Entities to be added")]
  public GameObject ragPlaceholder;
  public GameObject anxPlaceholder;
  public GameObject depPlaceholder;

  public List<GameObject> objects;

  void Start()
  {
    this.gameObject.SetActive(false);
    this.enabled = false;
  }
}
