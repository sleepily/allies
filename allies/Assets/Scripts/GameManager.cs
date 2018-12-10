using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public InputManager im;
  public UIManager ui;
  public PlayerManager pm;
  public LevelManager lm;
  public CameraManager cm;

  private void Start()
  {
    im.gm = this;
    ui.gm = this;
    pm.gm = this;
    lm.gm = this;
    cm.gm = this;
  }
}
