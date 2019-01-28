using UnityEngine;
using System.Collections;

public class Frustration : CombinedCharacter
{
  public MagmaTear magmaTearPrefab;

  public bool used = false;

  protected override void Ability()
  {
    base.Ability();

    if (used)
      return;

    ShootTear(magmaTearPrefab);

    used = true;
  }
}
