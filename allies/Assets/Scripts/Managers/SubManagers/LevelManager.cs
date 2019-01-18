using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SubManager
{
  public List<string> levels;
  public bool foundActiveScene = false;

  [Header("DO NOT touch this")]
  public SpriteRenderer colliderSpriteRenderer;

  public override void Init()
  {
    base.Init();
  }

  public void Retry()
  {
    gameManager.sceneManager.RetryLevel();
  }

  public void LoadNextLevel()
  {
    gameManager.sceneManager.LoadNextLevel();
  }
}