using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrybabyTear : Tear
{
  public override void Init()
  {
    base.Init();

    ModifyRigidBody();
    ModifyPolygonCollider();
  }

  private void Update()
  {
    velocity = rb.velocity;
    RotateSpriteAngle();
  }

  void ModifyRigidBody()
  {
    rb.isKinematic = false;
    rb.gravityScale = gameManager.characterManager.globalGravityScale / 2;
  }

  void ModifyPolygonCollider()
  {
    polygonCollider2D.enabled = false;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckForWater(collision);
    CheckForFireFlower(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckForWater(collision);
    CheckForFireFlower(collision);
  }

  void CheckForWater(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Water"))
      return;
    
    Water water = collision.GetComponent<Water>();
    water.parent.Activate();

    Destroy(this.gameObject);
  }

  void CheckForFireFlower(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("FireFlower"))
      return;

    var continousFlower = collision.GetComponent<ContinousFlower>();

    if (!continousFlower)
      return;

    continousFlower.Deactivate();
  }
}
