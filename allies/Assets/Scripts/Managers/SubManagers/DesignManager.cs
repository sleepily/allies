using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesignManager : SubManager
{
  List<FMNObject> objects = new List<FMNObject>();

  private void Awake()
  {
    if (LoadMainScreenInPlaymode())
      return;

    Init();
  }

  bool LoadMainScreenInPlaymode()
  {
    if (!GameManager.globalGameManager)
    {
      Debug.Log("GGM doesn't exist. Loading scene 0.");
      UnityEngine.SceneManagement.SceneManager.LoadScene(0);
      return true;
    }

    return false;
  }

  public override void Init()
  {
    DisableDesignManager();
  }

  void DisableDesignManager()
  {
    gameObject.SetActive(false);
    enabled = false;
  }
}