using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactable
{
  public FloatableObject parent;
  public SpriteRenderer spriteRenderer;
  public BoxCollider2D boxCollider;

  public override void Init()
  {
    base.Init();
    GetAllComponents();
    CreateBoxCollider();
    this.transform.SetParent(parent.transform);
  }

  void GetAllComponents()
  {
    boxCollider = GetComponent<BoxCollider2D>();

    if (!boxCollider)
      boxCollider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;

    spriteRenderer = GetComponent<SpriteRenderer>();

    if (!spriteRenderer)
      Debug.Log("Cannot find sprite renderer?");
  }

  void CreateBoxCollider()
  {
    if (polygonCollider2D)
      Destroy(polygonCollider2D);

    boxCollider.size = spriteRenderer.size;
    boxCollider.isTrigger = true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckCharacterCollision(collision);
  }

  void CheckCharacterCollision(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    gameManager.sceneManager.RetryLevelOnKill();
  }
}
