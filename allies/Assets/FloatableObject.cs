using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatableObject : Interactable
{
  public GameObject water;
  SpriteRenderer waterSpriteRenderer;

  public float floatTime = 1f;
  public float floatHeight = 4f;

  Vector2 initialPosition;
  Vector2 initialWaterScale;

  public override void Init()
  {
    base.Init();

    initialPosition = this.transform.position;
    waterSpriteRenderer = water.GetComponent<SpriteRenderer>();
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

    this.transform.position += (Vector3)((Vector2.up * floatHeight) * (Time.deltaTime / floatTime));
    waterSpriteRenderer.size += ((Vector2.up * floatHeight) * (Time.deltaTime / floatTime));
  }
}
