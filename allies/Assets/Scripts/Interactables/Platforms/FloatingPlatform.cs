using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : FloatableObject
{
  public Water water;
  Vector2 initialWaterScale;
  float waterColliderOffset = .2f;

  public override void Init()
  {
    base.Init();
    SetWaterProperties();
  }

  void SetWaterProperties()
  {
    if (!water)
      water = GetComponentInChildren<Water>();

    water.parent = this;
    initialWaterScale = water.spriteRenderer.size;
  }

  public override void Deactivate()
  {
    return;
  }

  protected override void Float()
  {
    FloatUp();
  }

  protected override void FloatUp()
  {
    base.FloatUp();
    ChangeWaterSize();
  }

  void ChangeWaterSize()
  {
    float mappedFloatHeight = Tools.ExtensionMethods.Map01(transform.position.y, position_initial.y, position_final.y);
    float addedHeight = mappedFloatHeight * floatHeight;
    water.spriteRenderer.size = new Vector2(initialWaterScale.x, initialWaterScale.y + addedHeight);
    water.boxCollider.size = water.spriteRenderer.size;
    water.boxCollider.offset = new Vector2(0, -waterColliderOffset + ((water.boxCollider.size.y - waterColliderOffset) / -2));
  }
}
