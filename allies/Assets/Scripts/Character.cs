using UnityEngine;

public class Character : MonoBehaviour
{
  public PlayerManager playerManager;

  [Header("Physics")]
  public Rigidbody2D rb;
  public bool allowJump;
  public bool allowMove;
  public bool isColliding;
  public bool isJumping;

  [Header("Animations")]
  public Animator animator;
  public bool mirrorAnimation;

  public State state;

  public enum State
  {
    idle,
    move,
    jump,
    stun,
    ability
  }

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    state = State.idle;
    SetAnimatorProperties();
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
    rb.gravityScale = playerManager.globalGravityScale;
  }

  private void SetAnimatorProperties()
  {
    if (animator == null)
      return;

    animator.SetBool("isColliding", isColliding);
    animator.SetBool("isJumping", isJumping);
    animator.SetFloat("verticalVelocity", rb.velocity.y);
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

    rb.velocity += horizontalForce * rb.gravityScale;
  }

  public void Jump(Vector2 verticalForce)
  {
    if (!allowJump || !isColliding)
      return;

    if (verticalForce.magnitude < 1)
      return;
    
    state = State.jump;
    isJumping = true;

    rb.velocity += verticalForce * rb.gravityScale;
    
  }

  private void CheckGroundCollision(Collision2D collision)
  {
    isColliding = false;

    // check for collision in the lower 30% of the collider in order to enable jumping
    foreach (ContactPoint2D cp in collision.contacts)
      if (cp.point.y < transform.position.y + (Vector2.down * .3f).y)
      {
        isColliding = true;
        isJumping = false;
        Debug.DrawRay(cp.point, cp.normal, Color.green);
      }
  }
}