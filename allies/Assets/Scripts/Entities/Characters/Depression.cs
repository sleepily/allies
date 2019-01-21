using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depression : Character
{
  public float jetpackOffset = .2f;
  public float jetpackForce = 1f;
  public float jetpackDuration = 2f;
  public float timestamp_jetpack = -1f;
  public bool jetpackIsActive = false;

  public float horizontalDamping = 1.4f;

  protected override void Ability()
  {
    base.Ability();

    if (abilityIndex == 0)
      CryBaby();
    if (abilityIndex == 1)
      JetPack();
  }

  void CryBaby()
  {
    // shoot crybaby tear
  }

  void JetPack()
  {
    if (timestamp_jetpack < 0)
    {
      timestamp_jetpack = Time.time;
      jetpackIsActive = true;
    }

    if (Time.time < timestamp_jetpack + jetpackOffset)
    {
      allowMove = false;
      return;
    }

    if (Time.time < timestamp_jetpack + jetpackOffset + jetpackDuration)
    {
      allowMove = true;
      rb.velocity = new Vector2(rb.velocity.x / horizontalDamping, jetpackForce);
      return;
    }

    timestamp_jetpack = -1f;
    rb.velocity = Vector2.zero;
    abilityIndex = 0;
    jetpackIsActive = false;
  }

  protected override void DeactivateAbility()
  {
    base.DeactivateAbility();
  }

  protected override void Update()
  {
    base.Update();

    DisableMoveWhenTouchingGround();
  }

  void DisableMoveWhenTouchingGround()
  {
    if (!jetpackIsActive)
      return;

    if (!isCollidingWithGround)
      return;
    
    allowMove = false;
  }
}
