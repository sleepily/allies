using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public GameManager gameManager;

  [Header("Physics")]
  public PolygonCollider2D polygonCollider2D;
  public Rigidbody2D rb;

  [Header("Sprites")]
  public SpriteRenderer spriteRenderer;
  public int spriteIndex = 0;
  public List<Sprite> sprites;
  
  [Header("Shooting")]
  public float speed = 1f;
  public float angle = 0f;
  protected Vector2 direction;
  public float shootingOffset = 1f;

  [Header("Rigidbody/Collision")]
  public bool isKinematic = false;
  public bool isShot = false;
  public bool isColliding = false;

  private void Awake()
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

  public virtual void Init()
  {
    GetSpriteRenderer();
    SetRandomSprite();
    CreateRigidBody();
    CreatePolygonCollider();
    gameManager = GameManager.globalGameManager;
    MoveToInteractablesManager();
  }

  protected virtual void Collide(Collision2D collision)
  {
    Debug.Log("Projectile collision");
    Destroy(this.gameObject);
  }

  void Move()
  {
    if (!isShot)
      return;

    if (isColliding)
      return;

    if (isKinematic)
      this.transform.position += (Vector3)direction * this.speed * Time.deltaTime;
  }

  public virtual void Shoot(Entity parent, Vector2 direction)
  {
    isShot = true;
    this.gameManager = parent.gameManager;
    this.direction = direction.normalized;
    this.angle = Vector2.Angle(Vector2.right, direction);
    this.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle);
    this.transform.position = (Vector2)parent.transform.position + (this.direction * shootingOffset);

    if (!rb)
      CreateRigidBody();

    if (!isKinematic)
      this.rb.velocity = direction * speed;
  }

  public void MoveToInteractablesManager()
  {
    transform.SetParent(gameManager.interactablesManager.transform);
  }

  private void GetSpriteRenderer()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void SetRandomSprite()
  {
    spriteIndex = Random.Range(0, sprites.Count);
    spriteRenderer.sprite = sprites[spriteIndex];
  }

  protected void CreateRigidBody()
  {
    if (!rb)
      rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;

    rb.isKinematic = isKinematic;
    rb.useFullKinematicContacts = isKinematic;
  }

  protected void CreatePolygonCollider()
  {
    polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
  }
}
