using UnityEngine;
using System.Collections;

public class DA : CombinedCharacter
{
  public IceTear iceTearPrefab;

  protected override void Ability()
  {
    ShootTear(iceTearPrefab);
  }
}
