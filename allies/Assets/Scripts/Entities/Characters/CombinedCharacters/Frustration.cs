using UnityEngine;
using System.Collections;

public class Frustration : CombinedCharacter
{
  public MagmaTear magmaTearPrefab;

  public bool used = false;

  protected override void Ability()
  {
    if (used)
      return;

    base.Ability();

    ShootTear(magmaTearPrefab);

    used = true;
  }

  protected override void ShootTear(Tear tearPrefab)
  {
    MagmaTear magmaTear = Instantiate(magmaTearPrefab);
    
    magmaTear.Shoot(this);
  }
}
