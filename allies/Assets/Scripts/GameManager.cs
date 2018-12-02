using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public UIManager ui;
  public PlayerManager playerManager;
  public LevelManager levelManager;

  private void Start()
  {
    ui.gm = this;
    playerManager.gm = this;
    levelManager.gm = this;
  }
}
