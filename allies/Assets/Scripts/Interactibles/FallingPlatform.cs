using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : Interactible
{
  [Header("Action")]
  public Platform platform;
  public List<Fuse> fuses;

  // prevent creation of rigidbody and collider
  public override void Init()
  {
    return;
  }

  override public void Action()
  {
    foreach (Fuse fuse in fuses)
      Destroy(fuse.gameObject);

    platform.rb.gravityScale = gameManager.playerManager.globalGravityScale;
    platform.rb.isKinematic = false;
  }
}
