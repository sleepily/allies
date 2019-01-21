using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : Interactable
{
  Depression depression;
  public GameObject water;
  public bool depressionPresent = false;

  public float floatTime = 1f;
  public float floatHeight = 4f;

  Vector2 initialPosition;

  public override void Init()
  {
    base.Init();

    initialPosition = this.transform.position;
  }

  private void Update()
  {
    if (depressionPresent)
      if (depression.abilityActive)
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
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckForDepression(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CheckForDepression(collision);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.name == "Depression")
      depressionPresent = false;
  }

  void CheckForDepression(Collision2D collision)
  {
    if (collision.gameObject.name != "Depression")
      return;

    if (!depression)
      depression = collision.gameObject.GetComponent<Depression>();

    depressionPresent = true;
  }
}
