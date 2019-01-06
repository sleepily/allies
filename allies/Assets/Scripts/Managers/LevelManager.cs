using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public GameManager gameManager;

  public int activeLevelIndex = 0;
  public List<string> levels;

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
        activeLevelIndex = SceneManager.GetActiveScene().buildIndex;
  }

  public void LoadLevel(int index)
  {
    if (index == activeLevelIndex)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
      return;
    }

    activeLevelIndex = index;
    SceneManager.LoadScene(levels[activeLevelIndex], LoadSceneMode.Single);
  }

  public void LoadNextLevel()
  {
    LoadLevel(activeLevelIndex + 1);
  }
}