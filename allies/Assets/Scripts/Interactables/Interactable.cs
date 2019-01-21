using UnityEngine;

public class Interactable : MonoBehaviour
{
  [HideInInspector]
  public GameManager gameManager;

  [HideInInspector]
  public PolygonCollider2D polygonCollider2D;
  [HideInInspector]
  public Rigidbody2D rb;

  [Header("Action")]
  public bool actionActivated = false;

  private void Start()
  {
    gameManager = GameManager.globalGameManager;

    if (!gameManager)
      Debug.LogError("No gamemanager");

    Init();
  }

  public virtual void Init()
  {
    CreateRigidBody();
    CreatePolygonCollider();
  }

  public virtual void Activate()
  {
    actionActivated = true;
  }

  public virtual void Deactivate()
  {
    actionActivated = false;
  }

  public void MoveToInteractablesManager()
  {
    transform.SetParent(gameManager.interactablesManager.transform);
  }

  private void CreateRigidBody()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();

    if (!rb)
      rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;

    rb.isKinematic = true;
    rb.useFullKinematicContacts = true;
  }

  private void CreatePolygonCollider()
  {
    polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
  }
}