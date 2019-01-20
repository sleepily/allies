using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : SubManager
{
  public enum Screen
  {
    splash,
    mainMenu,
    levelSelect,
    level,
    glossary,
    options,
    pause,
    quit
  }

  public Screen screen;

  [Header("If Screen = Level")]
  public int levelID = 0;

  AsyncOperation asyncLoad;
  AsyncOperation asyncUnload;

  public override void Init()
  {
    base.Init();
    
    LoadScreen(screen);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.N))
      LoadLevelFromBuildIndex(levelID - 1);
    if (Input.GetKeyDown(KeyCode.M))
      LoadLevelFromBuildIndex(levelID + 1);
  }

  public void LoadScreen(Screen screen)
  {
    this.screen = screen;

    string sceneID = "";

    switch (screen)
    {
      case Screen.splash:
        sceneID = "SplashScreen";
        LoadScreenSingle(sceneID);
        break;
      case Screen.mainMenu:
        sceneID = "MenuScreen";
        LoadScreenSingle(sceneID);
        break;
      case Screen.levelSelect:
        /*
        sceneID = "SelectionScreen";
        break;
        */
        LoadLevelFromBuildIndex(levelID);
        return;
      case Screen.glossary:
        sceneID = "GlossaryScreen";
        LoadScreenSingle(sceneID);
        break;
      case Screen.options:
        sceneID = "OptionsScreen";
        LoadScreenAdditive("OptionsScreen");
        break;
      case Screen.pause:
        sceneID = "PauseScreen";
        LoadScreenAdditive("PauseScreen");
        break;
      case Screen.quit:
        Quit(); //TODO: replace with scene
        break;
      case Screen.level:
        LoadLevelFromBuildIndex(levelID);
        return;
    }
  }

  void Quit()
  {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #else
      Application.Quit ();
    #endif
  }

  public void LoadScene(Scene scene, LoadSceneMode mode)
  {
    StartCoroutine(LoadSceneAsync(scene.buildIndex, mode));
  }

  public void RetryLevel()
  {
    LoadScreenSingle(this.levelID);
  }

  public void LoadNextLevel()
  {
    this.levelID++;
    LoadScreenSingle(this.levelID);
  }

  public void LoadLevelFromBuildIndex(int levelID)
  {
    this.levelID = levelID;
    LoadScreenSingle(this.levelID);
  }

  void LoadScreenSingle(Scene scene)
  {
    StartCoroutine(LoadSceneAsync(scene.buildIndex, LoadSceneMode.Single));
  }

  void LoadScreenSingle(int sceneBuildIndex)
  {
    StartCoroutine(LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single));
  }

  void LoadScreenSingle(string sceneName)
  {
    StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Single));
  }

  public void LoadScreenAdditive(Scene scene)
  {
    StartCoroutine(LoadSceneAsync(scene.buildIndex, LoadSceneMode.Additive));
  }

  public void LoadScreenAdditive(string sceneName)
  {
    StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Additive));
  }

  IEnumerator LoadSceneAsync(string sceneID, LoadSceneMode sceneMode)
  {
    asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneID, sceneMode);
    
    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }

  IEnumerator LoadSceneAsync(int sceneBuildIndex, LoadSceneMode sceneMode)
  {
    asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneBuildIndex, sceneMode);
    
    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }

  IEnumerator UnloadSceneAsync(Scene scene)
  {
    asyncUnload = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
    
    while (!asyncUnload.isDone)
    {
      yield return null;
    }
  }
}
