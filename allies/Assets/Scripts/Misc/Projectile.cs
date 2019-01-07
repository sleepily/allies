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

  public float speed = 1f;
  public float angle = 0f;
  Vector2 direction;

  public bool isShot = false;
  public bool isColliding = false;

  private void Start()
  {
    Init();
  }

  private void Update()
  {
    Move();
  }

  public void Init()
  {
    GetSpriteRenderer();
    SetRandomSprite();
    CreateRigidBody();
    CreatePolygonCollider();
  }

  void Move()
  {
    if (!isShot)
      return;

    this.transform.position += (Vector3)direction * this.speed * Time.deltaTime;
  }

  public void Shoot(GameObject parent, float angle)
  {
    isShot = true;
    this.angle = angle;
    direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
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
