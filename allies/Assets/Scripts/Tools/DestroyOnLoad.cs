using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
  private void Awake()
  {
    if (GameManager.globalGameManager)
      Destroy(this.gameObject);
  }
}
