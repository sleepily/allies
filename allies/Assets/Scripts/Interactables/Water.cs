using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactable
{
  public FloatableObject parent;
  public SpriteRenderer spriteRenderer;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public override void Init()
  {
    base.Init();
    polygonCollider2D.isTrigger = true;
    this.transform.SetParent(parent.transform);
  }
}
