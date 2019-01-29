using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : Projectile
{
  protected float initialAngle;
  public Vector2 velocity;
  public Vector2 shootVelocity;

  public override void Init()
  {
    base.Init();

    initialAngle = transform.rotation.eulerAngles.z;
  }

  protected void RotateSpriteAngle()
  {
    angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

    angle += initialAngle;

    if (angle < 0)
      angle += 360;

    Quaternion newRotation = this.transform.rotation;
    newRotation.eulerAngles = new Vector3(0, 0, angle);
    this.transform.rotation = newRotation;
  }

  public virtual void Shoot(Character parent)
  {
    isShot = true;
    shootVelocity = new Vector2(parent.isMovingLeft ? -shootVelocity.x : shootVelocity.x, shootVelocity.y);
    Vector2 direction = shootVelocity.normalized;

    this.gameManager = parent.gameManager;
    this.angle = Vector2.Angle(Vector2.right, direction);
    this.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle);
    this.transform.position = (Vector2)parent.transform.position + shootingOffset;

    if (!rb)
      CreateRigidBody();

    this.rb.velocity = shootVelocity * speed;
  }
}
