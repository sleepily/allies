using UnityEngine;

public class Interactable : FMNObject
{
  [HideInInspector]
  public PolygonCollider2D polygonCollider2D;
  [HideInInspector]
  public Rigidbody2D rb;

  [Header("Action")]
  public bool actionActivated = false;

  public override void Init()
  {
    gameManager = GameManager.globalGameManager;
    InitRigidBody();
    InitPolygonCollider();
    MoveToParentTransform();
    initialized = true;
  }

  public override void MoveToParentTransform()
  {
    transform.SetParent(gameManager.interactablesManager.transform);
  }

  public virtual void Activate()
  {
    actionActivated = true;
  }

  public virtual void Deactivate()
  {
    actionActivated = false;
  }

  private void InitRigidBody()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();

    if (!rb)
      rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;

    rb.isKinematic = true;
    rb.useFullKinematicContacts = true;
    rb.gravityScale = GameManager.globalGravityScale;
  }

  private void InitPolygonCollider()
  {
    polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();

    if (!polygonCollider2D)
      polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
  }
}