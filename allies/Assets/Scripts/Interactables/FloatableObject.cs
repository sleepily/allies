using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatableObject : Interactable
{
  public Water water;

  public float floatTime = 1f;
  public float floatHeight = 4f;

  Vector2 initialPosition;
  Vector2 initialWaterScale;

  public override void Init()
  {
    base.Init();

    water.parent = this;
    initialPosition = this.transform.position;
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.G))
      Activate();

    FloatUp();
  }

  void FloatUp()
  {
    if (!actionActivated)
      return;

    if (transform.position.y >= initialPosition.y + floatHeight)
      return;

    ChangePosition();
    ChangeWaterSize();
  }

  void ChangePosition()
  {
    this.transform.position += (Vector3)((Vector2.up * floatHeight) * (Time.deltaTime / floatTime));
  }

  void ChangeWaterSize()
  {
    water.spriteRenderer.size += ((Vector2.up* floatHeight) * (Time.deltaTime / floatTime));
    water.boxCollider.size = water.spriteRenderer.size;
    water.boxCollider.offset = new Vector2(0, water.boxCollider.size.y / -2);
  }
}
