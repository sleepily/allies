using UnityEngine;
using System.Collections.Generic;

public class Stress : CombinedCharacter
{
  Vector3 distance_start;

  public override void ActivateAbility()
  {
    base.ActivateAbility();

    SaveCharacterDistanceAtStart();
  }

  void SaveCharacterDistanceAtStart()
  {
    distance_start = combination[0].transform.position - combination[1].transform.position;
  }

  protected override void Ability()
  {
    base.Ability();
    
    Vector2 abilityDirection = isMovingLeft ? Vector2.left : Vector2.right;

    rb.velocity = new Vector2(abilityDirection.x * 24, rb.velocity.y);
  }

  public override void DeactivateAbility()
  {
    base.DeactivateAbility();

    SetNewCharacterPositions();
  }

  void SetNewCharacterPositions()
  {
    combination[0].transform.position = transform.position;
    combination[1].transform.position = transform.position + distance_start;
  }
}
