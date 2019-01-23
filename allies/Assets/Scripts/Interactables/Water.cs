using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactable
{
  public FloatableObject parent;
  public SpriteRenderer spriteRenderer;
  public BoxCollider2D boxCollider;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public override void Init()
  {
    base.Init();
    CreateBoxCollider();
    polygonCollider2D.isTrigger = true;
    this.transform.SetParent(parent.transform);
  }

  void CreateBoxCollider()
  {
    if (polygonCollider2D)
      Destroy(polygonCollider2D);

    boxCollider = GetComponent<BoxCollider2D>();

    if (!boxCollider)
      boxCollider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckCharacterCollision(collision);
  }

  void CheckCharacterCollision(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    gameManager.sceneManager.RetryLevel();
  }
}
