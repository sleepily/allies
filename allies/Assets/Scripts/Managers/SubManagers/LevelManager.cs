using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SubManager
{
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