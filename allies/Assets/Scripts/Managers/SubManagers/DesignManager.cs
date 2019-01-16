﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignManager : SubManager
{
  [Header("Level Properties")]
  public string levelName = "";
  
  public List<CharacterPlaceholder> characterPlaceholders;

  public GameObject designParent;
  List<Interactable> interactables;
  List<Entity> entities;

  public override void Init()
  {
    base.Init();

    AssignToGamemanager();
    LoadChildrenToLists();
    AssignGamemanagerToChildren();
    gameManager.InitializeManagers();
    MoveChildrenToInterabtablesManager();
    DisableDesignManager();
  }

  void AssignToGamemanager()
  {
    gameManager.designManager = this;
  }

  void LoadChildrenToLists()
  {
    foreach (Interactable interactable in FindObjectsOfType<Interactable>())
      interactables.Add(interactable);
    foreach (Entity entity in FindObjectsOfType<Entity>())
      entities.Add(entity);
  }

  public void AssignGamemanagerToChildren()
  {
    foreach (Interactable interactable in interactables)
      interactable.gameManager = this.gameManager;
    foreach (Entity entity in entities)
      entity.gameManager = this.gameManager;
  }

  public void MoveChildrenToInterabtablesManager()
  {
    foreach (Interactable i in designParent.GetComponentsInChildren<Interactable>())
      i.MoveToInteractablesManager();
    foreach (Entity e       in designParent.GetComponentsInChildren<Entity>())
      e.MoveToInteractablesManager();
  }

  void DisableDesignManager()
  {
    gameObject.SetActive(false);
    enabled = false;
    Destroy(this.gameObject);
  }
}