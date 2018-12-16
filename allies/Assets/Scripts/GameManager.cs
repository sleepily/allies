using UnityEngine;

public class GameManager : MonoBehaviour
{
  public InputManager im;
  public UIManager ui;
  public PlayerManager pm;
  public LevelManager lm;
  public CameraManager cm;

  public State state;
  public State lastState;

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
    ChangeState(State.level, gameObject);

    im.gm = this;
    ui.gm = this;
    pm.gm = this;
    lm.gm = this;
    cm.gm = this;
  }

  private void Update()
  {
    CheckState();
  }

  private void CheckState()
  {
    if (state == lastState)
      return;

    switch (state)
    {
      case State.splash:
        break;

      case State.title:
        break;

      case State.level:
        break;

      case State.pause:
        break;

      default:
        PrintBadStateError(state);
        break;
    }

    lastState = state;
  }

  public void ChangeState(State state, GameObject id)
  {
    this.state = state;
    PrintLogMessage(id);
  }

  private void PrintLogMessage(GameObject id)
  {
    string logstring = "{0} changed current state to {1}.";
    logstring = string.Format(logstring, id.name, state.ToString());
    Debug.Log(logstring);
  }

  private void PrintBadStateError(State badState)
  {
    string errorString = "Could not switch to state {0}: State does not exist.";
    errorString = string.Format(errorString, badState.ToString());
    Debug.LogError(errorString);
  }
}