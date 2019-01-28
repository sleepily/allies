using UnityEngine;
using System.Collections;

public class Apathy : CombinedCharacter
{
  public IceTear iceTearPrefab;

  protected override void Ability()
  {
    base.Ability();

    ShootTear(iceTearPrefab);
  }
}
