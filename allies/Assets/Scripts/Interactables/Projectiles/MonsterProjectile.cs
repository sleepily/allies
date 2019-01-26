using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : Projectile
{
  protected override void Collide(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
    {
      isColliding = true;
      Destroy(this.gameObject);
    }
  }

  public void Bounce()
  {
    this.gameObject.layer = LayerMask.NameToLayer("Projectiles");
    this.gameObject.tag = "Projectile";
    this.direction *= -1;
  }
}
