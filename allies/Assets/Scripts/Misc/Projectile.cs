using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public GameManager gameManager;

  public PolygonCollider2D polygonCollider2D;
  public Rigidbody2D rb;
  public SpriteRenderer spriteRenderer;

  public List<Sprite> sprites;

  public bool isKinematic = false;

  public bool isShot = false;
  public bool isColliding = false;

  private void Start()
  {
    Init();
  }

  public void Init()
  {
    GetSpriteRenderer();
    SetRandomSprite();
    CreateRigidBody();
    CreatePolygonCollider();
  }

  public void Shoot(GameObject parent)
  {
    float angle = 0;
    Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    this.transform.SetPositionAndRotation(parent.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
  }

  public void MoveToInteractiblesManager()
  {
    transform.SetParent(gameManager.interactiblesManager.transform);
  }

  private void GetSpriteRenderer()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void SetRandomSprite()
  {
    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
  }

  private void CreateRigidBody()
  {
    rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    rb.isKinematic = isKinematic;
    rb.useFullKinematicContacts = isKinematic;
  }

  private void CreatePolygonCollider()
  {
    polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
  }
}
