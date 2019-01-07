using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaTear : Tear
{
  public List<Sprite> coldSprites;
  
  private void CreateRigidBody()
  {
    rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    rb.isKinematic = isKinematic;
    rb.useFullKinematicContacts = false;
  }

  void Collide(Collision2D collision)
  {
    //dont do anything if tear collides with character
    if (collision.gameObject.CompareTag("Character"))
      return;

    isColliding = true;
  }
}
