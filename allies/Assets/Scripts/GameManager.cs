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

  public State state;

  public enum State
  {
    splash,
    title,
    options,
    select_world,
    select_level,
    level,
    pause,
    cutscene
  }

  private void Start()
  {
    im.gm = this;
    ui.gm = this;
    pm.gm = this;
    lm.gm = this;
    cm.gm = this;
  }

  public void ChangeState(State state, GameObject id)
  {
    this.state = state;
    PrintLogMessage(id);
  }

  void PrintLogMessage(GameObject id)
  {
    string logstring = "%s changed current state to %s.";
    string.Format(logstring, id.name, state.ToString());
    Debug.Log(logstring);
  }
}
