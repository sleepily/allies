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

  public Type type;

  override protected void Collide(Collision2D collision)
  {
    isColliding = true;

    switch(type)
    {
      case Type.ice:
        Destroy(this.gameObject);
        break;
      case Type.magma:
        this.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        break;
      default:
        break;
    }
  }
}
