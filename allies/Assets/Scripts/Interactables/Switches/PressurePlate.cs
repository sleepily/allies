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
    // prevent activation by icey tears
    return;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    ActivateSwitchOnTagTrigger("Character", collision);
    ActivateSwitchOnTagTrigger("MovableStone", collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    ActivateSwitchOnTagTrigger("Character", collision);
    ActivateSwitchOnTagTrigger("MovableStone", collision);
  }

  private void OnTriggerExit2D(Collider2D collision)
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
