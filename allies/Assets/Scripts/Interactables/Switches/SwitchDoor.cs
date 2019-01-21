using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : Interactable
{
  SpriteRenderer spriteRenderer;

  public override void Init()
  {
    base.Init();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public override void Activate()
  {
    base.Activate();
    //TODO: implement animation
    spriteRenderer.color = new Color(1, 1, 1, 0);
    polygonCollider2D.isTrigger = true;
  }

  public override void Deactivate()
  {
    base.Deactivate();
    spriteRenderer.color = Color.white;
    polygonCollider2D.isTrigger = false;
  }
}
