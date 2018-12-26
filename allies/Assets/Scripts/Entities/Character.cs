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
  public bool deactivateAbility;

  public enum State
  {
    idle,
    move,
    jump,
    movejump,
    stun,
    ability
  }

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
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

  /*
   * useful for possible gravity changes in future levels
   * should not affect performance, since only called in FixedUpdate()
   */
  private void GetGlobalGravityScale()
  {
    rb.gravityScale = playerManager.globalGravityScale;
  }

  //update the animator variables used to transition between animations
  private void SetAnimatorProperties()
  {
    if (animator == null)
      return;

    animator.SetBool("isColliding", isColliding);
    animator.SetBool("isJumping", isJumping);
    animator.SetFloat("verticalVelocity", rb.velocity.y);

    if (rb.velocity.x < .1 && rb.velocity.y < .1)
      state = State.idle;
  }

  public void CheckAbilityStatus()
  {
    if (state != State.ability)
    {
      //use boolean as trigger (bool deactivateAbility will be reset in DeactivateAbility())
      if (deactivateAbility)
        DeactivateAbility();

      return;
    }

    allowJump = false;
    allowMove = false;

    switch (name)
    {
      case "Rage":
        Rampage();
        break;
    }
  }

  public void Move(Vector2 horizontalForce)
  {
    if (!allowMove)
      return;

    rb.velocity += horizontalForce * rb.gravityScale;

    if (Mathf.Abs(rb.velocity.x) > .1)
    {
      state = State.move;

      if (Mathf.Abs(rb.velocity.y) > .1)
        state = State.movejump;
    }
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

  void DeactivateAbility()
  {
    //TODO: continue this
    allowJump = true;
    allowMove = true;

    deactivateAbility = false;
  }

  void Rampage()
  {
    //TODO: implement direction checking and bouncing off walls
    rb.AddForce(Vector2.right * 30);
  }
}