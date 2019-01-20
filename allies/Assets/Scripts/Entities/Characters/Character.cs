using UnityEngine;

public class Character : Entity
{
  public PlayerManager playerManager;

  [Header("Physics")]
  public Rigidbody2D rb;
  public Vector2 velocity;
  public bool allowJump;
  public bool allowMove;
  public bool isCollidingWithGround;
  public bool isCollidingWithWall;
  public bool isJumping;

  [Header("Animations")]
  public SpriteRenderer spriteRenderer;
  public Animator animator;
  float horizontalVelocityAbs, verticalVelocityAbs;
  public bool isMovingLeft;
  public bool isMoving;
  public bool abilityActive;
  public int abilityIndex = 0;
  public float angle;
  float angleVelocityThreshold = 1f;

  private void Start()
  {
    GetAllComponents();
    isMovingLeft = false;
  }

  protected virtual void Update()
  {
    CheckMovementDirection();
    CheckAbilityStatus();
    SetAnimatorProperties();
  }

  private void FixedUpdate()
  {
    GetGlobalGravityScale();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    AllCollisionChecks(collision);
  }

  protected virtual void AllCollisionChecks(Collision2D collision)
  {
    CheckCollisionWithEnemy(collision);
    CheckCollisionWithCharacter(collision);
    CheckCollisionWithGround(collision);
    CheckCollisionWithWall(collision);
  }

  protected virtual void GetAllComponents()
  {
    rb          = GetComponent<Rigidbody2D>();
    animator    = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    ResetCollision();
  }

  void ResetCollision()
  {
    isCollidingWithGround = false;
    isCollidingWithWall = false;
  }
  
  private void GetGlobalGravityScale()
  {
    if (!gameManager.playerManager)
      return;

    rb.gravityScale = gameManager.playerManager.globalGravityScale;
  }
  
  private void SetAnimatorProperties()
  {
    if (animator == null)
      return;

    CharacterSpecificAnimationProperties();
    GetAbsoluteVelocityValues();
    PreventIdleAnimationWhileJumping(); //TODO: work this into blend tree somehow
    SetAnimatorVariables();
    MirrorSpriteIfMovingLeft();
  }

  void SetAnimatorVariables()
  {
    animator.SetBool   ("abilityActive", abilityActive);
    animator.SetInteger("abilityIndex", abilityIndex);
    animator.SetBool   ("isJumping", isJumping);
    animator.SetBool   ("isMoving", CharacterIsMoving());
    animator.SetBool   ("mirrorAnimation", isMovingLeft);
    animator.SetFloat  ("horizontalVelocity", rb.velocity.x);
    animator.SetFloat  ("verticalVelocity", rb.velocity.y);
    animator.SetFloat  ("horizontalVelocityAbs", horizontalVelocityAbs);
    animator.SetFloat  ("verticalVelocityAbs", verticalVelocityAbs);
  }

  void PreventIdleAnimationWhileJumping()
  {
    // prevent idle and walking animation while jumping
    if (isJumping)
    {
      horizontalVelocityAbs = 0f;

      if (verticalVelocityAbs <= .1f)
        verticalVelocityAbs = -1f;
    }
  }

  void GetAbsoluteVelocityValues()
  {
    // positive velocity values for correctly triggering blend tree motions
    horizontalVelocityAbs = Mathf.Abs(rb.velocity.x);
    verticalVelocityAbs = Mathf.Abs(rb.velocity.y);
  }

  protected virtual void CharacterSpecificAnimationProperties()
  {

  }

  protected float CalculateCharacterAngle()
  {
    if
    (
      Mathf.Abs(rb.velocity.x) < angleVelocityThreshold &&
      Mathf.Abs(rb.velocity.y) < angleVelocityThreshold
    )
      return 0f;

    return Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
  }

  protected float GetVelocityAngle()
  {
    float angle = 90f;

    if (!isMoving)
      return angle;

    angle = Vector2.Angle(Vector2.down, rb.velocity.normalized);

    return angle;
  }

  bool CharacterIsMoving()
  {
    if (velocity.Equals(Vector2.zero))
      return false;

    return true;
  }

  void CheckMovementDirection()
  {
    if (!abilityActive)
    {
      if (velocity.x < -.1)
        isMovingLeft = true;
      if (velocity.x >  .1)
        isMovingLeft = false;
    }
  }

  public void ActivateAbility()
  {
    if (!isCollidingWithGround)
      return;

    abilityActive = true;
  }

  protected void CheckAbilityStatus()
  {
    if (!abilityActive)
    {
      DeactivateAbility();
      return;
    }

    Ability();
  }

  public void Move(float axisHorizontal)
  {
    if (!allowMove)
      return;

    velocity.x = axisHorizontal;

    float moveForce = velocity.x * rb.gravityScale * gameManager.playerManager.movementForce;

    rb.velocity = new Vector2(moveForce, rb.velocity.y);
  }

  public void Jump(float axisVertical)
  {
    if (!allowJump)
      return;

    if (isJumping)
      return;

    if (axisVertical < .5f)
      return;

    velocity.y = axisVertical;

    float jumpForce = velocity.y * rb.gravityScale * gameManager.playerManager.jumpForce;
    
    isJumping = true;

    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
  }

  protected virtual void CheckCollisionWithCharacter(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    if (abilityActive)
      abilityActive = false;
  }

  private void CheckCollisionWithGround(Collision2D collision)
  {
    isCollidingWithGround = false;

    // check for collision in the lower 25% of the collider in order to enable jumping
    foreach (ContactPoint2D cp in collision.contacts)
      if (cp.point.y < transform.position.y + (Vector2.down * .75f).y)
      {
        isCollidingWithGround = true;
        isJumping = false;
        Debug.DrawRay(cp.point, cp.normal, Color.green);
      }
  }

  private void CheckCollisionWithWall(Collision2D collision)
  {
    isCollidingWithWall = false;

    if (!abilityActive)
      return;
    
    if (!collision.gameObject.CompareTag("Platform"))
    {
      // isMovingLeft = !isMovingLeft;
      // rb.velocity = new Vector2(isMovingLeft ? -30 : 30, 0); // fix bounce caused by using rb.velocity.y
      rb.velocity = new Vector2(0, 0); // fix bounce caused by using rb.velocity.y
      return;
    }

    // check for collision in the upper collider (except top) in order to enable bouncing off walls
    foreach (ContactPoint2D cp in collision.contacts)
      if
      (
        cp.point.y < transform.position.y + (Vector2.up   * .45f).y &&
        cp.point.y > transform.position.y + (Vector2.down * .35f).y
      )
      {
        isCollidingWithWall = true;
        Debug.DrawRay(cp.point, cp.normal, Color.blue);

        if (abilityActive)
        {
          isMovingLeft = (cp.point.x < transform.position.x);

          rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
      }
  }
  
  protected virtual void CheckCollisionWithEnemy(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Enemy"))
      return;

    gameManager.levelManager.Retry();
  }

  void MirrorSpriteIfMovingLeft()
  {
    if (!spriteRenderer)
      return;

    spriteRenderer.flipX = isMovingLeft;
  }

  protected virtual void Ability()
  {
    allowJump = false;
    allowMove = false;
  }

  // resets all character's properties to default behaviour and resets the switch
  protected virtual void DeactivateAbility()
  {
    allowJump = true;
    allowMove = true;

    abilityActive = false;

    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  protected void ShootTear(Tear tearPrefab)
  {
    Tear tear = Instantiate(tearPrefab);
    tear.Shoot(this, gameManager.inputManager.angleToMouse, gameManager.inputManager.toMouse.normalized);
    DeactivateAbility();
  }
}