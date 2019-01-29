using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTear : Tear
{
  protected override void Collide(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("IceTear"))
      return;

    isColliding = true;
    Destroy(this.gameObject);
  }
}
