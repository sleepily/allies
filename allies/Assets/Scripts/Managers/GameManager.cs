﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager
{
  public static GameManager globalGameManager;

  public SceneManager sceneManager;
  public CameraManager cameraManager;
  public static Camera globalCamera;

  [HideInInspector]
  public DesignManager designManager;
  [HideInInspector]
  public LevelManager levelManager;
  [HideInInspector]
  public InputManager inputManager;
  [HideInInspector]
  public UIManager uiManager;
  [HideInInspector]
  public CharacterManager characterManager;
  [HideInInspector]
  public InteractablesManager interactablesManager;

  [HideInInspector]
  public List<Manager> managers;

  [Header("Prefabs")]
  public LevelManager levelManagerPrefab;
  public InputManager inputManagerPrefab;
  public UIManager uiManagerPrefab;
  public CharacterManager playerManagerPrefab;
  public CameraManager cameraManagerPrefab;
  public InteractablesManager interactablesManagerPrefab;

  private void Awake()
  {
    GameManager.globalGameManager = this;
    globalCamera = GetComponentInChildren<Camera>();
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
    characterManager = Instantiate(playerManagerPrefab);
    interactablesManager = Instantiate(interactablesManagerPrefab);
  }

  void AddManagersToList()
  {
    managers.Add(levelManager);
    managers.Add(inputManager);
    managers.Add(uiManager);
    managers.Add(characterManager);
    managers.Add(levelManager);
    managers.Add(cameraManager);
    managers.Add(interactablesManager);
  }

  void SetGamemanagerInChildren()
  {
    levelManager.gameManager = this;
    inputManager.gameManager = this;
    uiManager.gameManager = this;
    characterManager.gameManager = this;
    levelManager.gameManager = this;
    interactablesManager.gameManager = this;
  }
}