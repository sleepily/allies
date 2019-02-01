using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteractable : MonoBehaviour
{
  Interactable parent;

  private void Awake()
  {
    parent = GetComponent<Interactable>();
  }

  private void Update()
  {
    if (!parent.initialized)
      return;

    if (Input.GetKeyDown(KeyCode.Space))
    {
      parent.Activate();

      Destroy(this);
    }
  }
}
