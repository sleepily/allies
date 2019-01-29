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

    water.parent = this;
  }

  public override void Deactivate()
  {
    return;
  }

  protected override void Float()
  {
    FloatUp();
    ChangeWaterSize();
  }

  void ChangeWaterSize()
  {
    water.spriteRenderer.size += ((Vector2.up * floatHeight) * (Time.deltaTime / floatTime));
    water.boxCollider.size = water.spriteRenderer.size;
    water.boxCollider.offset = new Vector2(0, -waterColliderOffset + ((water.boxCollider.size.y - waterColliderOffset) / -2));
  }
}
