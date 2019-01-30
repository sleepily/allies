using UnityEngine;

public class Elevator : FloatableObject
{
  public Sprite spriteActivated;
  public Sprite spriteDeactivated;

  SpriteRenderer spriteRenderer;

  public override void Init()
  {
    base.Init();
    GetComponents();
  }

  void GetComponents()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public override void Deactivate()
  {
    spriteRenderer.sprite = spriteDeactivated;
    return;
  }

  public override void Activate()
  {
    base.Activate();
    spriteRenderer.sprite = spriteActivated;
  }
}