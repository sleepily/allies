using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public GameManager gameManager;

  public int levelIndex = 0;
  public List<string> levels;

  [Header("DO NOT touch this")]
  public SpriteRenderer colliderSpriteRenderer;

  private void Start()
  {
    LoadAllScenesToList();
    FindCurrentScene();
  }

  //TODO: add scenes by path (Assets>Scenes>Levels)
  void LoadAllScenesToList()
  {
    levels = new List<string>();

    foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
    {
      if (S.enabled)
      {
        string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 6);
        levels.Add(name);
      }
    }
  }

  void FindCurrentScene()
  {
    //TODO: get scene naes as string, sort out everything without "level" in its name, sort alphabetically
    foreach (string level in levels)
      if (level == SceneManager.GetActiveScene().name)
        levelIndex = SceneManager.GetActiveScene().buildIndex;
  }

  public void LoadLevel(int index)
  {
    if (index == levelIndex)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
      return;
    }

    levelIndex = index;
    SceneManager.LoadScene(levels[levelIndex], LoadSceneMode.Single);
  }

  public void LoadNextLevel()
  {
    LoadLevel(levelIndex++);
  }
}