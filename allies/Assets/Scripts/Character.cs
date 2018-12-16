using UnityEngine;

public class Character : MonoBehaviour
{
  public PlayerManager playerManager;

  [Header("Physics")]
  public bool allowJump;
  public bool allowMove;
  public Rigidbody2D rigidBody;
  public bool isColliding;

  [Header("Animations")]
  public Animator animator;
  public bool mirrorAnimation;

  public enum State
  {
    idle,
    move,
    jump,
    stun,
    ability
  }

  public State state;

  private void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    state = State.idle;
  }

  private void FixedUpdate()
  {
    GetGlobalGravityScale();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckGroundCollision(collision);
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
    rigidBody.gravityScale = playerManager.globalGravityScale;
  }

  public void Move(Vector2 horizontalForce)
  {
    if (!allowMove)
      return;

    if (state == State.jump)
    {
      if (isColliding)
        state = State.move;
      else
        state = State.jump;
    }

    rigidBody.velocity += horizontalForce * rigidBody.gravityScale;
  }

  public void Jump(Vector2 verticalForce)
  {
    if (!allowJump || !isColliding)
      return;

    state = State.jump;

    rigidBody.velocity += verticalForce * rigidBody.gravityScale;
  }

  private void CheckGroundCollision(Collision2D collision)
  {
    isColliding = false;

    // check for collision in the lower 30% of the collider in order to enable jumping
    foreach (ContactPoint2D cp in collision.contacts)
      if (cp.point.y < transform.position.y + (Vector2.down * .3f).y)
      {
        isColliding = true;
        Debug.DrawRay(cp.point, cp.normal, Color.green);
      }
  }
}