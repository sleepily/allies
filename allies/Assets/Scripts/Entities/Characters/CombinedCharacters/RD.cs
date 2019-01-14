using UnityEngine;
using System.Collections;

public class RD : CombinedCharacter
{
  public MagmaTear magmaTearPrefab;

  protected override void Ability()
  {
    ShootTear(magmaTearPrefab);
  }
}
