using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : SubManager
{
  public enum Screen
  {
    splash,
    mainMenu,
    options,
    levelSelect,
    level,
    pause
  }

  public Screen screen;

  [Header("If Screen = Level")]
  public string levelID = "";

  AsyncOperation asyncLoad;
  AsyncOperation asyncUnload;

  public override void Init()
  {
    base.Init();
    
    LoadScreen(screen);
  }

  public void LoadScreen(Screen screen)
  {
    this.screen = screen;

    string sceneID = "";

    switch (screen)
    {
      case Screen.splash:
        sceneID = "SplashScreen";
        break;
      case Screen.mainMenu:
        sceneID = "MenuScreen";
        break;
      case Screen.level:
        sceneID = levelID;
        break;
    }

    Debug.Log("Loading Scene: " + sceneID);
    LoadScreenSingle(sceneID);
  }

  public void LoadLevel(string levelID)
  {
    this.levelID = levelID;
    LoadScreen(Screen.level);
  }

  public void LoadScreenSingle(string sceneID)
  {
    StartCoroutine(LoadSceneAsync(sceneID, LoadSceneMode.Single));
  }

  public void LoadScreenAdditive(string sceneID)
  {
    StartCoroutine(LoadSceneAsync(sceneID, LoadSceneMode.Additive));
  }

  IEnumerator LoadSceneAsync(string sceneID, LoadSceneMode sceneMode)
  {
    asyncLoad = SceneManager.LoadSceneAsync(sceneID, sceneMode);

    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }

  IEnumerator UnloadSceneAsync(Scene scene)
  {
    asyncUnload = SceneManager.UnloadSceneAsync(scene);

    while (!asyncUnload.isDone)
    {
      yield return null;
    }
  }
}
