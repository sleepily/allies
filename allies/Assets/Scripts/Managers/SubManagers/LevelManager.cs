using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SubManager
{
  public int activeLevelBuildIndex = 0;
  public List<string> levels;
  public bool foundActiveScene = false;

  [Header("DO NOT touch this")]
  public SpriteRenderer colliderSpriteRenderer;

  public override void Init()
  {
    base.Init();

    // LoadAllScenesToList();
    FindCurrentScene();
  }

  /*
  void LoadAllScenesToList()
  {
    foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
    {
      if (S.enabled)
      {
        string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 6);
        levels.Add(name);
      }
    }
    
    var info = new DirectoryInfo(Application.dataPath + "/Scenes/Levels/");

    var files = info.GetFiles();

    foreach (var f in files)
    {
      if (f.Extension == ".meta")
        continue;

      string fileName = f.Name;

      fileName = fileName.Replace(".unity", "");

      levels.Add(fileName);
    }
  }
  */

  void FindCurrentScene()
  {
    //TODO: get scene names as string, sort out everything without "level" in its name, sort alphabetically
    foreach (string level in levels)
      if (level == SceneManager.GetActiveScene().name)
      {
        activeLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
        foundActiveScene = true;
      }
  }

  public void LoadLevel(int index)
  {
    activeLevelBuildIndex = index;
    SceneManager.LoadScene(levels[activeLevelBuildIndex], LoadSceneMode.Single);
  }

  public void Retry()
  {
    if (!foundActiveScene)
      return;

    LoadLevel(activeLevelBuildIndex);
  }

  public void LoadNextLevel()
  {
    if (!foundActiveScene)
      return;

    LoadLevel(activeLevelBuildIndex + 1);
  }
}