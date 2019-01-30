using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager
{
  public static GameManager globalGameManager;

  [Header("Playtesting")]
  public bool isPlaytest = false;

  [Header("Instantiated Managers")]
  public SceneManager sceneManager;
  public CameraManager cameraManager;

  [Header("Global/Static Variables")]
  public static Camera globalCamera;
  public static bool globalPlaytestActive;
  
  public DesignManager designManager;
  public LevelManager levelManager;
  public InputManager inputManager;
  public UIManager uiManager;
  public CharacterManager characterManager;
  public InteractablesManager interactablesManager;

  [HideInInspector]
  public List<Manager> managers;

  [Header("Prefabs")]
  public LevelManager levelManagerPrefab;
  public InputManager inputManagerPrefab;
  public UIManager uiManagerPrefab;
  public CharacterManager playerManagerPrefab;
  public InteractablesManager interactablesManagerPrefab;

  private void Awake()
  {
    SetStaticVariables();
    Debug.Log("Gamemanager awake");
  }

  void SetStaticVariables()
  {
    GameManager.globalGameManager = this;
    GameManager.globalPlaytestActive = isPlaytest;
    globalCamera = GetComponentInChildren<Camera>();
    Debug.Log("Gamemanager static variables");
  }

  public void InitializeManagersForPlay()
  {
    Debug.Log("Gamemanager instantiate managers for play");
    InstantiateManagers();
    InitializeDesignManager();
    AddManagersToList();
    // PrintDebugLog();
  }

  void InstantiateManagers()
  {
    levelManager = Instantiate(levelManagerPrefab);
    inputManager = Instantiate(inputManagerPrefab);
    uiManager = Instantiate(uiManagerPrefab);
    interactablesManager = Instantiate(interactablesManagerPrefab);
    characterManager = Instantiate(playerManagerPrefab);
  }

  void InitializeDesignManager()
  {
    designManager = FindObjectOfType<DesignManager>();
    designManager.Init();
  }

  void AddManagersToList()
  {
    managers.Clear();
    managers.Add(levelManager);
    managers.Add(inputManager);
    managers.Add(uiManager);
    managers.Add(characterManager);
    managers.Add(levelManager);
    managers.Add(cameraManager);
    managers.Add(interactablesManager);
    managers.Add(designManager);
  }

  void PrintDebugLog()
  {
    Debug.Log("All " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)).Length);
    Debug.Log("  GameObjects " + Resources.FindObjectsOfTypeAll(typeof(GameObject)).Length);
    Debug.Log("  Components " + Resources.FindObjectsOfTypeAll(typeof(Component)).Length);
    Debug.Log("  AudioClips " + Resources.FindObjectsOfTypeAll(typeof(AudioClip)).Length);
    Debug.Log("  Interactables " + Resources.FindObjectsOfTypeAll(typeof(Interactable)).Length);
    Debug.Log("  Entities " + Resources.FindObjectsOfTypeAll(typeof(Entity)).Length);
    Debug.Log("    Characters " + Resources.FindObjectsOfTypeAll(typeof(Character)).Length);
  }
}