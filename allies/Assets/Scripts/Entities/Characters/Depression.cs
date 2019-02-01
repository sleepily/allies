using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depression : Character
{
  [Header("Crybaby")]
  public CrybabyTear crybabyTearPrefab;
  public bool crybabyActivated = false;

  [Header("Jetpack")]
  public float jetpackOffset = .2f;
  public float jetpackForce = 1f;
  public float jetpackDuration = 2f;
  public float timestamp_jetpack = -1f;
  public bool jetpackActivated = false;
  public bool jetpackIsActive = false;
  public bool jetpackBeforeCrybaby = false;

  public float horizontalDamping = 1.4f;

  public override void ActivateAbility()
  {
    if (!canUseAbilityInAir)
      if (!isCollidingWithGround)
        return;

    abilityActive = true;

    if (abilityIndex == 0)
      gameManager.characterManager.SetNextCharacterAsActive();
  }

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
    if (jetpackBeforeCrybaby)
      return;

    if (crybabyActivated)
      return;

    crybabyActivated = true;

    ShootTear(crybabyTearPrefab);
  }

  protected override void ShootTear(Tear tearPrefab)
  {
    CrybabyTear crybabyTear = Instantiate(crybabyTearPrefab);
    
    crybabyTear.Shoot(this);
  }

  void JetPack()
  {
    jetpackBeforeCrybaby = true;
    jetpackActivated = true;

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

    EndJetpack();
  }

  void EndJetpack()
  {
    timestamp_jetpack = -1f;
    rb.velocity = Vector2.zero;
    abilityIndex = 0;
    jetpackIsActive = false;

    if (gameManager.characterManager.activeCharacter == this)
      gameManager.characterManager.SetNextCharacterAsActive();
  }

  public override void DeactivateAbility()
  {
    base.DeactivateAbility();

    crybabyActivated = false;

    jetpackActivated = false;
    jetpackIsActive = false;
    jetpackBeforeCrybaby = false;
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
