using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
  public enum EditorVariable
  {
    yes,
    no
  }

  public EditorVariable destroyInEditor;

  private void Awake()
  {
    if (GameManager.globalGameManager || destroyInEditor == EditorVariable.yes)
      Destroy(this.gameObject);
  }
}
