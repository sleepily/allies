using System.Collections;
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
    credits,
    quit
  }

  public Screen screen;

  [Header("First Level BuildIndex")]
  public int levelID = 0;
  public string levelName = "Intro_001";

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
        gameManager.soundManager.musicSelector = SoundManager.MusicSelector.none;
        LoadScreenSingle(sceneID);
        break;
      case Screen.mainMenu:
        sceneID = "MenuScreen";
        gameManager.soundManager.musicSelector = SoundManager.MusicSelector.menu;
        LoadScreenSingle(sceneID);
        break;
      case Screen.credits:
        sceneID = "CreditsScreen";
        gameManager.soundManager.musicSelector = SoundManager.MusicSelector.credits;
        LoadScreenSingle(sceneID);
        break;
      case Screen.levelSelect:
        gameManager.soundManager.musicSelector = SoundManager.MusicSelector.levelSelection;
        sceneID = "SelectionScreen";
        LoadScreenSingle(sceneID);
        break;
      case Screen.level:
        gameManager.soundManager.musicSelector = SoundManager.MusicSelector.level;
        LoadLevelFromBuildIndex(levelID);
        return;
      case Screen.quit:
        gameManager.soundManager.musicSelector = SoundManager.MusicSelector.none;
        Quit();
        break;
    }
  }

  void Quit()
  {
    if (transitioning)
      return;

    StartCoroutine(QuitCoroutine());
  }

  IEnumerator QuitCoroutine()
  {
    transitioning = true;

    gameManager.cameraManager.FadeOut(Color.black);

    yield return new WaitForSeconds(.5f);

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
              Application.Quit ();
    #endif
  }

  public void FinishLevel()
  {
    if (transitioning)
      return;

    SoundManager.PlayOneShot(gameManager.soundManager.level_finish, gameManager.soundManager.uiAudioSource);

    StartCoroutine(FinishLevelCoroutine(.5f, Color.black));
  }

  public void RetryLevel()
  {
    if (transitioning)
      return;

    SoundManager.PlayOneShot(gameManager.soundManager.level_retry, gameManager.soundManager.uiAudioSource);

    StartCoroutine(RetryLevelCoroutine(.5f, Color.black));
  }

  public void RetryLevelOnKill()
  {
    if (transitioning)
      return;

    SoundManager.PlayOneShot(gameManager.soundManager.level_fail, gameManager.soundManager.uiAudioSource);

    StartCoroutine(RetryLevelCoroutine(0, new Color(.5f, 0, 0, 1)));
  }

  IEnumerator RetryLevelCoroutine(float waitTime, Color fadeColor)
  {
    transitioning = true;

    gameManager.cameraManager.FadeOut(fadeColor);

    yield return new WaitForSeconds(waitTime);

    gameManager.cameraManager.ResetCameraPosition();

    transitioning = false;

    LoadScreenSingleAsLevel(this.levelName, fadeColor);
  }

  IEnumerator FinishLevelCoroutine(float waitTime, Color fadeColor)
  {
    transitioning = true;

    gameManager.cameraManager.FadeOut(fadeColor);

    yield return new WaitForSeconds(waitTime);

    gameManager.cameraManager.ResetCameraPosition();

    transitioning = false;

    LoadNextLevel();
  }

  public void LoadNextLevel()
  {
    this.levelID++;

    if (this.levelID >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
    {
      LoadScreenSingle("CreditsScreen");
      return;
    }

    LoadScreenSingleAsLevel(this.levelID, Color.black);
  }

  public void LoadLevelFromBuildIndex(int levelID)
  {
    this.levelID = levelID;
    LoadScreenSingleAsLevel(this.levelID, Color.black);
  }

  public void LoadLevelFromName(string levelName)
  {
    this.levelName = levelName;
    LoadScreenSingleAsLevel(levelName, Color.black);
  }

  void LoadScreenSingleAsLevel(int sceneBuildIndex, Color fadeColor)
  {
    gameManager.cameraManager.FadeIn(fadeColor);
    StartCoroutine(LoadLevelAsync(sceneBuildIndex, LoadSceneMode.Single));
  }

  void LoadScreenSingleAsLevel(string levelName, Color fadeColor)
  {
    gameManager.cameraManager.FadeIn(fadeColor);
    StartCoroutine(LoadLevelAsync(levelName, LoadSceneMode.Single));
  }

  void LoadScreenSingle(string sceneName)
  {
    gameManager.cameraManager.FadeIn(Color.black);
    StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Single));
  }

  IEnumerator LoadSceneAsync(string sceneID, LoadSceneMode sceneMode)
  {
    gameManager.cameraManager.FadeOut(Color.black);

    yield return new WaitForSeconds(.5f);

    gameManager.cameraManager.ResetCameraPosition();

    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneID, sceneMode);
  }

  IEnumerator LoadSceneAsync(int sceneBuildIndex, LoadSceneMode sceneMode)
  {
    gameManager.cameraManager.FadeOut(Color.black);

    yield return new WaitForSeconds(.5f);

    gameManager.cameraManager.ResetCameraPosition();

    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneBuildIndex, sceneMode);
  }

  IEnumerator LoadLevelAsync(int sceneBuildIndex, LoadSceneMode sceneMode)
  {
    gameManager.soundManager.musicSelector = SoundManager.MusicSelector.level;

    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneBuildIndex, sceneMode);

    UpdateLevelInfo();

    gameManager.InitializeManagersForPlay();
  }

  IEnumerator LoadLevelAsync(string levelName, LoadSceneMode sceneMode)
  {
    gameManager.soundManager.musicSelector = SoundManager.MusicSelector.level;

    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelName, sceneMode);

    UpdateLevelInfo();

    gameManager.InitializeManagersForPlay();
  }

  void UpdateLevelInfo()
  {
    UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

    levelName = activeScene.name;
    levelID = activeScene.buildIndex;
  }
}
