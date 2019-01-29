using UnityEngine;
using System.Collections;

public class Apathy : CombinedCharacter
{
  public IceTear iceTearPrefab;

  public float shootingFrequency = 1f;
  float timestamp_start = -1f;
  float timestamp_lastShot = -1f;

  protected override void Ability()
  {
    base.Ability();

    if (timestamp_start < 0)
    {
      timestamp_start = Time.time;
      timestamp_lastShot = timestamp_start - shootingFrequency;
    }

    if (Time.time > timestamp_lastShot + shootingFrequency)
    {
      ShootTear(iceTearPrefab);
      timestamp_lastShot += shootingFrequency;
    }
  }
}
