using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
  public bool done = false;

  private void Awake()
  {
    DontDestroyOnLoad(this.gameObject);
    done = true;
  }
}
