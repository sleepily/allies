using UnityEngine;

public class Character : Entity
{
  [HideInInspector]
  public PlayerManager playerManager;

  [HideInInspector]
  public bool startWithAbility = false;

  [Header("Physics")]
  public Rigidbody2D rb;

  public Vector2 velocity;
  public bool allowJump;
  public bool allowMove;
  public bool isCollidingWithGround;
  public bool isCollidingWithWall;
  public PhysicsMaterial2D moveMaterial;
  public PhysicsMaterial2D idleMaterial;

  public bool canUseAbilityInAir;
  public bool isJumping;

  [Header("Animations")]
  public SpriteRenderer spriteRenderer;

  public Animator animator;
  private float horizontalVelocityAbs, verticalVelocityAbs;
  public bool isMovingLeft;
  public bool isMoving;
  public bool abilityActive;
  public int abilityIndex = 0;
  public float angle;
  private float angleVelocityThreshold = 1f;

  private void Start()
  {
    Init();

    if (startWithAbility)
      abilityActive = true;
  }

  protected virtual void Init()
  {
    GetAllComponents();
    isMovingLeft = false;
  }

  protected virtual void GetAllComponents()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
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
    rb.sharedMaterial = idleMaterial;

    CheckForCharacterDistance();
  }

  protected virtual void CheckForCharacterDistance()
  {
    if (playerManager.activeCharactersInLevel.Count == 1)
      return;

    if (!abilityActive)
      return;

    foreach (Character character in playerManager.activeCharactersInLevel)
    {
      if (character == this)
        continue;

      float distance = Vector2.Distance(this.transform.position, character.transform.position);

      if (distance < 2f)
      {
        DeactivateAbility();
        return;
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    AllCollisionChecks(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
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

  //TODO: remove this
  private void CheckCollisionWithWall(Collision2D collision)
  {
    isCollidingWithWall = false;

    if (!collision.gameObject.CompareTag("Platform"))
    {
      if (!abilityActive)
        return;

      rb.velocity = new Vector2(0, 0);
      return;
    }

    // check for collision in the upper collider (except top) in order to enable bouncing off walls
    foreach (ContactPoint2D cp in collision.contacts)
      if
      (
        cp.point.y < transform.position.y + (Vector2.up * .45f).y &&
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

  private void OnCollisionExit2D(Collision2D collision)
  {
    ResetCollision();
  }

  private void ResetCollision()
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

  private void SetAnimatorVariables()
  {
    animator.SetBool("abilityActive", abilityActive);
    animator.SetInteger("abilityIndex", abilityIndex);
    animator.SetBool("isJumping", isJumping);
    animator.SetBool("isMoving", CharacterIsMoving());
    animator.SetBool("mirrorAnimation", isMovingLeft);
    animator.SetFloat("horizontalVelocity", rb.velocity.x);
    animator.SetFloat("verticalVelocity", rb.velocity.y);
    animator.SetFloat("horizontalVelocityAbs", horizontalVelocityAbs);
    animator.SetFloat("verticalVelocityAbs", verticalVelocityAbs);
  }

  private void PreventIdleAnimationWhileJumping()
  {
    // prevent idle and walking animation while jumping
    if (isJumping)
    {
      horizontalVelocityAbs = 0f;

      if (verticalVelocityAbs <= .1f)
        verticalVelocityAbs = -1f;
    }
  }

  private void GetAbsoluteVelocityValues()
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

  private bool CharacterIsMoving()
  {
    if (velocity.Equals(Vector2.zero))
      return false;

    return true;
  }

  private void CheckMovementDirection()
  {
    if (!abilityActive)
    {
      if (velocity.x < -.1)
        isMovingLeft = true;
      if (velocity.x > .1)
        isMovingLeft = false;
    }
  }

  public void ActivateAbility()
  {
    if (!canUseAbilityInAir)
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

    rb.sharedMaterial = moveMaterial;

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

    rb.sharedMaterial = moveMaterial;

    velocity.y = axisVertical;

    float jumpForce = velocity.y * rb.gravityScale * gameManager.playerManager.jumpForce;

    isJumping = true;

    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
  }

  protected virtual void MirrorSpriteIfMovingLeft()
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

  protected virtual void ShootTear(Tear tearPrefab)
  {
    Tear tear = Instantiate(tearPrefab);
    tear.Shoot(this, gameManager.inputManager.toMouse.normalized);
  }
}