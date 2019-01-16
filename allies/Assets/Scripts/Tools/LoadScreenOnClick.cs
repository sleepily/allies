using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreenOnClick : MonoBehaviour
{
  public SceneManager.Screen screenToLoad;

  public void LoadScreen()
  {
    GameManager.globalGameManager.sceneManager.LoadScreen(screenToLoad);
  }
}
