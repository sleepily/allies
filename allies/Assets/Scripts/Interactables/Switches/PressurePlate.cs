using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Switch
{
  public override void Init()
  {
    base.Init();
    spriteRenderer.sprite = spriteDeactivated;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    ActivateSwitchOnTagCollision("Character", collision);
    ActivateSwitchOnTagCollision("MovableStone", collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    ActivateSwitchOnTagCollision("Character", collision);
    ActivateSwitchOnTagCollision("MovableStone", collision);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    DeactivateSwitch();
  }

  protected override void ActivateSwitch()
  {
    spriteRenderer.sprite = spriteActivated;
    base.ActivateSwitch();
  }

  protected override void DeactivateSwitch()
  {
    spriteRenderer.sprite = spriteDeactivated;
    base.DeactivateSwitch();
  }
}
