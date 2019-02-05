﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : SubManager
{
  bool transitioning = false;

  public enum Screen
  {
    splash,
    mainMenu,
    levelSelect,
    level,
    pause,
    quit
  }

  public Screen screen;

  [Header("First Level BuildIndex")]
  public int levelID = 0;

  public override void Init()
  {
    base.Init();
    
    LoadScreen(screen);
  }

  private void Update()
  {
    PlaytestControls();
  }

  void PlaytestControls()
  {
    if (!GameManager.globalPlaytestActive)
      return;

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
        sceneID = "SelectionScreen";
        break;
      case Screen.pause:
        sceneID = "PauseScreen";
        LoadScreenAdditive("PauseScreen");
        break;
      case Screen.quit:
        Quit();
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

  public void FinishLevel()
  {
    if (transitioning)
      return;

    StartCoroutine(FinishLevelCoroutine(.5f, Color.black));
  }

  public void RetryLevel()
  {
    if (transitioning)
      return;

    StartCoroutine(RetryLevelCoroutine(.5f, Color.black));
  }

  public void RetryLevelOnKill()
  {
    if (transitioning)
      return;

    StartCoroutine(RetryLevelCoroutine(0, new Color(.5f, 0, 0, 1)));
  }

  IEnumerator RetryLevelCoroutine(float waitTime, Color fadeColor)
  {
    transitioning = true;

    gameManager.cameraManager.FadeOut(fadeColor);

    yield return new WaitForSeconds(waitTime);

    transitioning = false;

    LoadScreenSingleAsLevel(this.levelID, fadeColor);
  }

  IEnumerator FinishLevelCoroutine(float waitTime, Color fadeColor)
  {
    transitioning = true;

    gameManager.cameraManager.FadeOut(fadeColor);

    yield return new WaitForSeconds(waitTime);

    transitioning = false;

    LoadNextLevel();
  }

  public void LoadNextLevel()
  {
    this.levelID++;
    LoadScreenSingleAsLevel(this.levelID, Color.black);
  }

  public void LoadLevelFromBuildIndex(int levelID)
  {
    this.levelID = levelID;
    LoadScreenSingleAsLevel(this.levelID, Color.black);
  }

  void LoadScreenSingleAsLevel(int sceneBuildIndex, Color fadeColor)
  {
    gameManager.cameraManager.FadeIn(fadeColor);
    StartCoroutine(LoadLevelAsync(sceneBuildIndex, LoadSceneMode.Single));
  }

  void LoadScreenSingle(Scene scene)
  {
    gameManager.cameraManager.FadeIn(Color.black);
    StartCoroutine(LoadSceneAsync(scene.buildIndex, LoadSceneMode.Single));
  }

  void LoadScreenSingle(int sceneBuildIndex)
  {
    gameManager.cameraManager.FadeIn(Color.black);
    StartCoroutine(LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single));
  }

  void LoadScreenSingle(string sceneName)
  {
    gameManager.cameraManager.FadeIn(Color.black);
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
    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneID, sceneMode);
  }

  IEnumerator LoadSceneAsync(int sceneBuildIndex, LoadSceneMode sceneMode)
  {
    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneBuildIndex, sceneMode);
  }

  IEnumerator LoadLevelAsync(int sceneBuildIndex, LoadSceneMode sceneMode)
  {
    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneBuildIndex, sceneMode);

    gameManager.InitializeManagersForPlay();
  }

  IEnumerator UnloadSceneAsync(Scene scene)
  {
    yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
  }
}
