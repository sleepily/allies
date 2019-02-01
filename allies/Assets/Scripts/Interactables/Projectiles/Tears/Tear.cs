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

  private void Update()
  {
    velocity = rb.velocity;
  }

  protected void RotateSpriteAngle()
  {
    angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

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
    this.direction = shootVelocity.normalized;

    this.gameManager = parent.gameManager;
    this.angle = Vector2.Angle(Vector2.right, direction);
    this.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + this.angle);
    this.transform.position = (Vector2)parent.transform.position + shootingOffset + (this.direction * shootingOffsetInDirection);

    if (!rb)
      CreateRigidBody();

    this.rb.velocity = shootVelocity * speed;
  }

  public virtual void Shoot(Character parent, bool toMouse = true)
  {
    isShot = true;
    Vector2 vectorToMouse = gameManager.inputManager.CalculateVectorBetweenCharacterAndMouse(parent);
    shootVelocity = vectorToMouse.normalized;
    this.direction = vectorToMouse.normalized;

    this.gameManager = parent.gameManager;
    this.angle = gameManager.inputManager.CalculateAngleToMouse();
    this.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + this.angle);
    this.transform.position = (Vector2)parent.transform.position + shootingOffset + (this.direction * shootingOffsetInDirection);

    if (!rb)
      CreateRigidBody();

    this.rb.velocity = shootVelocity * speed;
  }
}
