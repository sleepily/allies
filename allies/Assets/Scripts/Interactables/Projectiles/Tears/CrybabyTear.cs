using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrybabyTear : Tear
{
  float initialAngle;

  public Vector2 shootVelocity;
  public Vector2 velocity;

  public override void Init()
  {
    base.Init();

    initialAngle = transform.rotation.eulerAngles.z;

    ModifyRigidBody();
    ModifyPolygonCollider();
  }

  private void Update()
  {
    velocity = rb.velocity;
    RotateSpriteAngle();
  }

  void RotateSpriteAngle()
  {
    float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

    if (angle < 0)
      angle += 360;

    Quaternion newRotation = this.transform.rotation;
    newRotation.eulerAngles = new Vector3(0, 0, initialAngle + angle);
    this.transform.rotation = newRotation;
  }

  void ModifyRigidBody()
  {
    rb.isKinematic = false;
    rb.gravityScale = gameManager.playerManager.globalGravityScale / 2;
  }

  void ModifyPolygonCollider()
  {
    polygonCollider2D.enabled = false;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckForWater(collision);
    CheckForFireFlower(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckForWater(collision);
    CheckForFireFlower(collision);
  }

  void CheckForWater(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Water"))
      return;
    
    Water water = collision.GetComponent<Water>();
    water.parent.Activate();

    Destroy(this.gameObject);
  }

  void CheckForFireFlower(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("FireFlower"))
      return;

    var continousFlower = collision.GetComponent<ContinousFlower>();

    if (!continousFlower)
      return;

    continousFlower.Deactivate();
  }

  public void Shoot(Depression parent)
  {
    isShot = true;
    shootVelocity = new Vector2(parent.isMovingLeft ? -shootVelocity.x : shootVelocity.x, shootVelocity.y);
    Vector2 direction = shootVelocity.normalized;

    this.gameManager = parent.gameManager;
    this.angle = Vector2.Angle(Vector2.right, direction);
    this.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle);
    this.transform.position = (Vector2)parent.transform.position + (direction * shootingOffset);

    if (!rb)
      CreateRigidBody();
    
    this.rb.velocity = shootVelocity * speed;
  }
}
