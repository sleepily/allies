using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [Header("Initialized (DO NOT touch this)")]
  public DesignManager designManager;

  [Header("Initialized from Prefabs (should be empty)")]
  public LevelManager levelManager;
  public InputManager inputManager;
  public UIManager uiManager;
  public PlayerManager playerManager;
  public CameraManager cameraManager;
  public InteractiblesManager interactiblesManager;

  public List<GameObject> managers;

  [Header("Prefabs (DO NOT touch this)")]
  public LevelManager levelManagerPrefab;
  public InputManager inputManagerPrefab;
  public UIManager uiManagerPrefab;
  public PlayerManager playerManagerPrefab;
  public CameraManager cameraManagerPrefab;
  public InteractiblesManager interactiblesManagerPrefab;

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

    levelManager          = Instantiate(levelManagerPrefab);
    inputManager          = Instantiate(inputManagerPrefab);
    uiManager             = Instantiate(uiManagerPrefab);
    playerManager         = Instantiate(playerManagerPrefab);
    cameraManager         = Instantiate(cameraManagerPrefab);
    interactiblesManager  = Instantiate(interactiblesManagerPrefab);

    managers.Add(levelManager.gameObject);
    managers.Add(inputManager.gameObject);
    managers.Add(uiManager.gameObject);
    managers.Add(playerManager.gameObject);
    managers.Add(levelManager.gameObject);
    managers.Add(cameraManager.gameObject);
    managers.Add(interactiblesManager.gameObject);

    levelManager.gameManager = this;
    inputManager.gameManager = this;
    uiManager.gameManager = this;
    playerManager.gameManager = this;
    levelManager.gameManager = this;
    cameraManager.gameManager = this;
    interactiblesManager.gameManager = this;

    foreach (GameObject m in managers)
      m.transform.SetParent(this.transform);
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

      case State.cutscene:
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