using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
  public GameManager gameManager;

  public PolygonCollider2D polygonCollider2D;
  public Rigidbody2D rb;

  void Start()
  {
    rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    rb.isKinematic = true;
    rb.useFullKinematicContacts = true;

    polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
  }

  public void MoveToInteractiblesManager()
  {
    transform.SetParent(gameManager.interactiblesManager.transform);
  }
}
