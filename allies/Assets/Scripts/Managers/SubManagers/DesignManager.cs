using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesignManager : SubManager
{
  [Header("Level Properties")]
  public string levelName = "";

  [Header("Current level's objects")]
  List<FMNObject> objects = new List<FMNObject>();
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
    foreach (FMNObject obj in FindObjectsOfType<FMNObject>())
      objects.Add(obj);
  }

  void InitializeChildren()
  {
    foreach (FMNObject obj in FindObjectsOfType<FMNObject>())
      obj.Init();
  }

  public void MoveChildrenToInterabtablesManager()
  {
    foreach (FMNObject obj in FindObjectsOfType<FMNObject>())
      obj.MoveToParentTransform();
  }

  void DisableDesignManager()
  {
    gameObject.SetActive(false);
    enabled = false;
    Destroy(this.gameObject);
  }
}