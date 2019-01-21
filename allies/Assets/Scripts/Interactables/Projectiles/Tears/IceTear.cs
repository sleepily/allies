using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTear : Tear
{
  protected override void Collide(Collision2D collision)
  {
    isColliding = true;
    Destroy(this.gameObject);
    
    //TODO: implement
    /*
    Destroy(this.rb);
    Destroy(this.polygonCollider2D);
    //play particle animation
    */
  }
}
