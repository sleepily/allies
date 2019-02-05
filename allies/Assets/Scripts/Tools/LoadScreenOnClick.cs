using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenOnClick : MonoBehaviour
{
  public SceneManager.Screen screenToLoad;

  public void LoadScreen()
  {
    // Debug.Log("Loading screen " + screenToLoad.ToString());

    if (screenToLoad == SceneManager.Screen.level)
    {
      GameManager.globalGameManager.sceneManager.levelName = gameObject.name;
      GameManager.globalGameManager.sceneManager.RetryLevel();
    }
    else
      GameManager.globalGameManager.sceneManager.LoadScreen(screenToLoad);
  }
}
