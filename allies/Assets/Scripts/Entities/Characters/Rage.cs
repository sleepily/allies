using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Character
{
  public GameObject flame;
  public Vector2 abilityDirection;

  protected override void CharacterSpecificAnimationProperties()
  {
    SetFireAngle();
  }

  void SetFireAngle()
  {
    angle = 0f; // CalculateCharacterAngle();

    flame.transform.rotation = Quaternion.Euler(0, 0, angle); //90f + angle
  }

  protected override void Ability()
  {
    base.Ability();

    abilityDirection = isMovingLeft ? Vector2.left : Vector2.right;

    rb.velocity = new Vector2(abilityDirection.x * 18, rb.velocity.y);
  }

  protected override void MirrorSpriteIfMovingLeft()
  {
    base.MirrorSpriteIfMovingLeft();

    flame.transform.localScale = new Vector3(isMovingLeft ? -1 : 1, 1, 1);
  }
}
