using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrybabyTear : Tear
{
  public override void Init()
  {
    base.Init();

    ModifyPolygonCollider();
  }

  void ModifyPolygonCollider()
  {
    polygonCollider2D.isTrigger = true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckForWater(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckForWater(collision);
  }

  void CheckForWater(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Water"))
      return;


  }
}
