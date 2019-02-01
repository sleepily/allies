using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : Interactable
{
  [Header("Action")]
  public Platform platform;
  public List<Fuse> fuses;

  // prevent creation of rigidbody and collider
  public override void Init()
  {
    gameManager = GameManager.globalGameManager;
    MoveToParentTransform();
  }

  public override void Activate()
  {
    if (actionActivated)
      return;

    actionActivated = true;

    foreach (Fuse fuse in fuses)
    {
      if (fuse)
        Destroy(fuse.gameObject);
    }

    platform.rb.gravityScale = GameManager.globalGravityScale;
    platform.rb.isKinematic = false;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckPlatformCollision(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CheckPlatformCollision(collision);
  }

  void CheckPlatformCollision(Collision2D collision)
  {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Tears"))
    {
      platform.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
      platform.rb.velocity = new Vector2(0, platform.rb.velocity.y);
      return;
    }

    platform.rb.isKinematic = true;
    platform.rb.constraints = RigidbodyConstraints2D.FreezeAll;
    platform.rb.velocity = Vector2.zero;
  }
}
