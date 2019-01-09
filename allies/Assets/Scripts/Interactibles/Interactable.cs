using UnityEngine;

public class Interactable : MonoBehaviour
{
  public GameManager gameManager;

  public PolygonCollider2D polygonCollider2D;
  public Rigidbody2D rb;

  private void Start()
  {
    Init();
  }

  public virtual void Init()
  {
    CreateRigidBody();
    CreatePolygonCollider();
  }

  public virtual void Action()
  {

  }

  public void MoveToInteractiblesManager()
  {
    transform.SetParent(gameManager.interactiblesManager.transform);
  }

  private void CreateRigidBody()
  {
    rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    rb.isKinematic = true;
    rb.useFullKinematicContacts = true;
  }

  private void CreatePolygonCollider()
  {
    polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
  }
}