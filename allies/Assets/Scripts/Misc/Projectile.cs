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

  private void OnCollisionEnter2D(Collision2D collision)
  {
    Collide(collision);
  }

  public void Init()
  {
    GetSpriteRenderer();
    SetRandomSprite();
    CreateRigidBody();
    CreatePolygonCollider();
  }

  void Collide(Collision2D collision)
  {
    Destroy(this.gameObject);
  }

  void Move()
  {
    if (!isShot)
      return;

    this.transform.position += (Vector3)direction * this.speed * Time.deltaTime;
  }

  public void Shoot(GameObject parent, float angle, Vector2 direction)
  {
    isShot = true;
    this.angle = angle;
    this.direction = direction.normalized;
    this.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle);
    this.transform.position = parent.transform.position;
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
    polygonCollider2D.isTrigger = true;
  }
}
