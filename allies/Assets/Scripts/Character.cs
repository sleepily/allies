using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Physics")]
  public bool allowJump;

  public bool allowMove;
  public Rigidbody2D rb;
  public bool isColliding;

  public enum State
  {
    idle,
    move,
    jump,
    stun,
    ability
  }

  public State state;

  public PlayerManager pm;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {

  }

  private void FixedUpdate()
  {
    GetGlobalGravityScale();
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CheckGroundCollision(collision);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    isColliding = false;
  }

  private void GetGlobalGravityScale()
  {
    rb.gravityScale = pm.globalGravityScale;
  }

  public void Move(Vector2 horizontalForce)
  {
    if (!allowMove)
      return;

    state = State.move;

    rb.velocity += horizontalForce * rb.gravityScale;
  }

  public void Jump(Vector2 verticalForce)
  {
    if (!allowJump || !isColliding)
      return;

    state = State.jump;

    rb.velocity += verticalForce * rb.gravityScale;
  }

  private void CheckGroundCollision(Collision2D collision)
  {
    isColliding = false;

    foreach (ContactPoint2D cp in collision.contacts)
      if (cp.point.y < transform.position.y + (Vector2.down * .3f).y)
      {
        isColliding = true;
        Debug.DrawRay(cp.point, cp.normal, Color.green);
      }
  }
}