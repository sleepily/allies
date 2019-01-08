using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : Projectile
{
  public enum Type
  {
    ice,
    magma
  }

  override protected void Collide(Collision2D collision)
  {
    isColliding = true;
    rb.constraints = RigidbodyConstraints2D.FreezeAll;
  }
}
