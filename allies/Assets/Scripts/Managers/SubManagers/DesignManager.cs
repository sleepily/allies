using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesignManager : SubManager
{
  [Header("Level Properties")]
  public string levelName = "";

  [Header("Current level's objects")]
  List<Interactable> interactables = new List<Interactable>();
  List<Entity> entities = new List<Entity>();

  private void Awake()
  {
    if (LoadMainScreenInPlaymode())
      return;

    AssignToGamemanager();
  }

  bool LoadMainScreenInPlaymode()
  {
    if (!GameManager.globalGameManager)
    {
      Debug.LogError("GGM doesn't exist. Loading scene 0.");
      UnityEngine.SceneManagement.SceneManager.LoadScene(0);
      return true;
    }

    return false;
  }

  private void Start()
  {
    // do not Init() here
    return;
  }

  public override void Init()
  {
    base.Init();

    LoadChildrenToLists();
    AssignGamemanagerToChildren();
    InitializeChildren();
    MoveChildrenToInterabtablesManager();

    DisableDesignManager();
  }

  void AssignToGamemanager()
  {
    GameManager.globalGameManager.designManager = this;
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

  void InitializeChildren()
  {
    foreach (Interactable interactable in interactables)
      interactable.Init();
    foreach (Entity entity in entities)
      entity.Init();
  }

  public void MoveChildrenToInterabtablesManager()
  {
    foreach (Interactable interactable in interactables)
      interactable.MoveToInteractablesManager();
    foreach (Entity entity in entities)
      entity.MoveToInteractablesManager();
  }

  void DisableDesignManager()
  {
    gameObject.SetActive(false);
    enabled = false;
    Destroy(this.gameObject);
  }
}