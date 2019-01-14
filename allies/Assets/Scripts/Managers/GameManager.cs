using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager
{
  public static GameManager globalGameManager;

  public GameSceneManager sceneManager;

  [HideInInspector]
  public DesignManager designManager;
  [HideInInspector]
  public LevelManager levelManager;
  [HideInInspector]
  public InputManager inputManager;
  [HideInInspector]
  public UIManager uiManager;
  [HideInInspector]
  public PlayerManager playerManager;
  [HideInInspector]
  public CameraManager cameraManager;
  [HideInInspector]
  public InteractablesManager interactablesManager;

  [HideInInspector]
  public List<Manager> managers;

  [Header("Prefabs")]
  public LevelManager levelManagerPrefab;
  public InputManager inputManagerPrefab;
  public UIManager uiManagerPrefab;
  public PlayerManager playerManagerPrefab;
  public CameraManager cameraManagerPrefab;
  public InteractablesManager interactablesManagerPrefab;

  private void Awake()
  {
    GameManager.globalGameManager = this;
  }

  private void Start()
  {

  }

  private void Update()
  {

  }

  public void InitializeManagers()
  {
    InstantiateManagers();
    AddManagersToList();
    SetGamemanagerInChildren();
  }

  void InstantiateManagers()
  {
    levelManager = Instantiate(levelManagerPrefab);
    inputManager = Instantiate(inputManagerPrefab);
    uiManager = Instantiate(uiManagerPrefab);
    playerManager = Instantiate(playerManagerPrefab);
    cameraManager = Instantiate(cameraManagerPrefab);
    interactablesManager = Instantiate(interactablesManagerPrefab);
  }

  void AddManagersToList()
  {
    managers.Add(levelManager);
    managers.Add(inputManager);
    managers.Add(uiManager);
    managers.Add(playerManager);
    managers.Add(levelManager);
    managers.Add(cameraManager);
    managers.Add(interactablesManager);
  }

  void SetGamemanagerInChildren()
  {
    levelManager.gameManager = this;
    inputManager.gameManager = this;
    uiManager.gameManager = this;
    playerManager.gameManager = this;
    levelManager.gameManager = this;
    cameraManager.gameManager = this;
    interactablesManager.gameManager = this;
  }
}