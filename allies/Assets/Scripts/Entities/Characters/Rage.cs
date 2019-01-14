using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Character
{
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

    if (isMovingLeft)
      rb.AddForce(Vector2.left * 30);
    else
      rb.AddForce(Vector2.right * 30);
  }
}
