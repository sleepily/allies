﻿using System.Collections;
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
  public static float globalGravityScale = 5f;
  
  public LevelManager levelManager;
  public InputManager inputManager;
  public UIManager uiManager;
  public CharacterManager characterManager;
  public InteractablesManager interactablesManager;

  [Header("Prefabs")]
  public LevelManager levelManagerPrefab;
  public InputManager inputManagerPrefab;
  public UIManager uiManagerPrefab;
  public InteractablesManager interactablesManagerPrefab;
  public CharacterManager characterManagerPrefab;

  private void Awake()
  {
    SetStaticVariables();
  }

  void SetStaticVariables()
  {
    GameManager.globalGameManager = this;
    GameManager.globalPlaytestActive = isPlaytest;
    globalCamera = GetComponentInChildren<Camera>();
  }

  // called from SceneManager after a level has been loaded
  public void InitializeManagersForPlay()
  {
    InstantiateManagers();
  }

  void InstantiateManagers()
  {
    levelManager = Instantiate(levelManagerPrefab);
    inputManager = Instantiate(inputManagerPrefab);
    uiManager = Instantiate(uiManagerPrefab);
    interactablesManager = Instantiate(interactablesManagerPrefab);
    characterManager = Instantiate(characterManagerPrefab);
  }
}