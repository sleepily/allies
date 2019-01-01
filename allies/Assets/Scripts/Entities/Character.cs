using UnityEngine;

public class Character : MonoBehaviour
{
  public PlayerManager playerManager;

  [Header("Physics")]
  public Rigidbody2D rb;
  public bool allowJump;
  public bool allowMove;
  public bool isCollidingWithGround;
  public bool isCollidingWithWall;
  public bool isJumping;

  [Header("Animations")]
  public Animator animator;
  public bool isMovingRight;

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
    CheckWallCollision(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CheckGroundCollision(collision);
    CheckWallCollision(collision);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    isCollidingWithGround = false;
    isCollidingWithWall = false;
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

    animator.SetBool  ("isColliding",         isCollidingWithGround);
    animator.SetBool  ("isJumping",           isJumping);
    animator.SetFloat ("horizontalVelocity",  rb.velocity.x);
    animator.SetFloat ("verticalVelocity",    rb.velocity.y);

    if (rb.velocity.x < .1 && rb.velocity.y < .1)
      if (state != State.ability)
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
      case "Anxiety":
        ColdFeet();
        break;
      case "Depression":
        Crybaby();
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
    if (!allowJump || !isCollidingWithGround)
      return;

    if (verticalForce.magnitude < 1)
      return;
    
    state = State.jump;
    isJumping = true;

    rb.velocity += verticalForce * rb.gravityScale;
  }

  private void CheckGroundCollision(Collision2D collision)
  {
    isCollidingWithGround = false;

    // check for collision in the lower 30% of the collider in order to enable jumping
    foreach (ContactPoint2D cp in collision.contacts)
      if (cp.point.y < transform.position.y + (Vector2.down * .3f).y)
      {
        isCollidingWithGround = true;
        isJumping = false;
        Debug.DrawRay(cp.point, cp.normal, Color.green);
      }
  }

  private void CheckWallCollision(Collision2D collision)
  {
    isCollidingWithWall = false;

    // check for collision in the middle 40% of the collider in order to enable bouncing off walls
    foreach (ContactPoint2D cp in collision.contacts)
      if
      (
        cp.point.y < transform.position.y + (Vector2.up   * .2f).y &&
        cp.point.y > transform.position.y + (Vector2.down * .2f).y
      )
      {
        isCollidingWithWall = true;
        Debug.DrawRay(cp.point, cp.normal, Color.blue);

        if (state == State.ability)
        {
          if (cp.point.x < transform.position.x)
            isMovingRight = true;
          else
            isMovingRight = false;
        }
      }
  }

  // resets all character's properties to default behaviour and resets the switch
  void DeactivateAbility()
  {
    allowJump = true;
    allowMove = true;

    rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    deactivateAbility = false;
  }

  void Rampage()
  {
    //TODO: implement direction checking and bouncing off walls
    if (isMovingRight)
      rb.AddForce(Vector2.right * 30);
    else
      rb.AddForce(Vector2.left  * 30);
  }

  void ColdFeet()
  {
    rb.constraints =
      RigidbodyConstraints2D.FreezePositionX |
      RigidbodyConstraints2D.FreezePositionY |
      RigidbodyConstraints2D.FreezeRotation;
  }

  void Crybaby()
  {
    //TODO: implement
  }
}