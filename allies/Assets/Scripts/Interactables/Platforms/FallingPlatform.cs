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
    return;
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

  private void Update()
  {
    CheckPlatformCollision();
  }

  void CheckPlatformCollision()
  {
    if (platform.rb.isKinematic)
      return;

    if (!platform.rb.IsTouchingLayers())
      return;

    platform.rb.isKinematic = true;
    platform.rb.constraints = RigidbodyConstraints2D.FreezeAll;
    platform.rb.velocity = Vector2.zero;
  }
}
