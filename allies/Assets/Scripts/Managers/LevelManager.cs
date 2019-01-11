using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public GameManager gameManager;

  public int activeLevelBuildIndex = 0;
  public List<string> levels;
  public bool foundActiveScene = false;

  [Header("DO NOT touch this")]
  public SpriteRenderer colliderSpriteRenderer;

  private void Start()
  {
    LoadAllScenesToList();
    FindCurrentScene();
  }

  void LoadAllScenesToList()
  {
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

  void FindCurrentScene()
  {
    //TODO: get scene naes as string, sort out everything without "level" in its name, sort alphabetically
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