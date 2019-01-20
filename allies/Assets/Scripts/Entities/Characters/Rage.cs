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
    angle = CalculateCharacterAngle();

    flame.transform.rotation = Quaternion.Euler(0, 0, 35f + angle); //90f + angle
  }

  protected override void Ability()
  {
    base.Ability();

    abilityDirection = isMovingLeft ? Vector2.left : Vector2.right;
    
    rb.AddForce(abilityDirection * 30);
  }
}
