using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Switch
{
  public Sprite spriteActivated;
  public Sprite spriteDeactivated;

  SpriteRenderer spriteRenderer;

  public override void Init()
  {
    base.Init();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void FixedUpdate()
  {
    DeactivateSwitch();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    ActivateSwitchOnTagCollision("Character", collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    ActivateSwitchOnTagCollision("Character", collision);
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
