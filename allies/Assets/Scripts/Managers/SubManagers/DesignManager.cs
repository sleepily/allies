using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesignManager : SubManager
{
  [Header("Level Properties")]
  public string levelName = "";
  
  public List<CharacterPlaceholder> characterPlaceholders;

  public GameObject designParent;
  List<Interactable> interactables = new List<Interactable>();
  List<Entity> entities = new List<Entity>();

  private void Awake()
  {
    LoadMainScreenInPlaymode();
  }

  void LoadMainScreenInPlaymode()
  {
    if (!GameManager.globalGameManager)
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
  }

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
    if (!gameManager)
      Debug.Log("GameManager missing, please open MainScene and change LevelID to test this level.");

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