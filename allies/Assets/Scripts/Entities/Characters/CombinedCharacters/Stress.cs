using UnityEngine;
using System.Collections;

public class Stress : CombinedCharacter
{
  protected override void Ability()
  {
    base.Ability();
    
    Vector2 abilityDirection = isMovingLeft ? Vector2.left : Vector2.right;

    rb.velocity = new Vector2(abilityDirection.x * 24, rb.velocity.y);
  }
}
