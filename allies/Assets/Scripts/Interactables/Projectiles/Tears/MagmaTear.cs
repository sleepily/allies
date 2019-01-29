using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaTear : Tear
{
  public float range = 8f;

  [Header("After Collision")]
  public ColdMagma coldMagmaPrefab;

  Vector2 position_initial;

  public override void Shoot(Character parent)
  {
    base.Shoot(parent);
    position_initial = parent.transform.position;
  }

  protected override void Collide(Collision2D collision)
  {
    isColliding = true;

    SpawnColdMagma();

    Destroy(this.gameObject);
  }

  private void Update()
  {
    RotateSpriteAngle();
    CheckRange();
  }

  void CheckRange()
  {
    Vector2 difference = (Vector2)transform.position - position_initial;
    float distance = Vector2.Distance(Vector2.zero, difference);

    Debug.DrawRay(position_initial, difference);

    if (distance <= range)
      return;

    Destroy(this.gameObject);
  }

  void SpawnColdMagma()
  {
    ColdMagma cold = Instantiate(coldMagmaPrefab);
    cold.Init(this);
  }
}
