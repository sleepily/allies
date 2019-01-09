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
  public bool isMovingLeft;
  public bool isMoving;
  public bool abilityActive;
  public GameObject flame;
  public float angle;
  public float angleVelocityThreshold;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    isMovingLeft = false;
  }

  private void Update()
  {
    CheckMovementDirection();
    SetAnimatorProperties();
  }

  private void FixedUpdate()
  {
    GetGlobalGravityScale();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckCharacterCollision(collision);
    CheckGroundCollision(collision);
    CheckWallCollision(collision);
    CheckEnemyCollision(collision);
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

  /*
   * useful for possible gravity changes in future levels
   */
  private void GetGlobalGravityScale()
  {
    if (!gameManager.playerManager)
      return;

    rb.gravityScale = gameManager.playerManager.globalGravityScale;
  }

  //update the animator variables used to transition between animations
  private void SetAnimatorProperties()
  {
    if (animator == null)
      return;

    CharacterSpecifics();

    // positive velocity values for correctly triggering blend tree motions
    float horizontalVelocityAbs = Mathf.Abs(rb.velocity.x);
    float verticalVelocityAbs = Mathf.Abs(rb.velocity.y);

    // prevent walking animation while jumping
    if (isJumping)
    {
      horizontalVelocityAbs = 0f;

      if (verticalVelocityAbs < 1f)
        verticalVelocityAbs = -1f;
    }


    animator.SetBool  ("abilityActive",         abilityActive);
    animator.SetBool  ("isJumping",             isJumping);
    animator.SetBool  ("isMoving",              CharacterIsMoving());
    animator.SetBool  ("mirrorAnimation",       isMovingLeft);
    animator.SetFloat ("horizontalVelocity",    rb.velocity.x);
    animator.SetFloat ("verticalVelocity",      rb.velocity.y);
    animator.SetFloat ("horizontalVelocityAbs", horizontalVelocityAbs);
    animator.SetFloat ("verticalVelocityAbs",   verticalVelocityAbs);

    FlipSpriteX();
  }

  void CharacterSpecifics()
  {
    if (name == "Rage")
      SetFireAngle();
  }

  float CalculateCharacterAngle()
  {
    if
    (
      Mathf.Abs(rb.velocity.x) < angleVelocityThreshold &&
      Mathf.Abs(rb.velocity.y) < angleVelocityThreshold
    )
      return -90f;

    return Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
  }

  void SetFireAngle()
  {
    angle = CalculateCharacterAngle();

    flame.transform.rotation = Quaternion.Euler(0, 0, 35f + angle); //90f + angle
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

  public void CheckAbilityStatus()
  {
    if (!abilityActive)
    {
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
      case "Eruption":
        Eruption();
        break;
      case "FrozenOutrage":
        FrozenOutrage();
        break;
    }
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

  private void CheckCharacterCollision(Collision2D collision)
  {
    if (name != "Anxiety")
      if (collision.gameObject.CompareTag("Character"))
        if (abilityActive)
          abilityActive = false;
  }

  private void CheckGroundCollision(Collision2D collision)
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

  private void CheckWallCollision(Collision2D collision)
  {
    isCollidingWithWall = false;

    if (!abilityActive)
      return;

    if (!collision.gameObject.CompareTag("Platform"))
    {
      isMovingLeft = !isMovingLeft;
      rb.velocity = new Vector2(isMovingLeft ? -30 : 30, 0); // fix bounce caused by using rb.velocity.y
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

  void CheckEnemyCollision(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Enemy"))
      return;

    if (name == "Anxiety" && abilityActive)
      collision.gameObject.SendMessage("Bounce");
    else
      gameManager.levelManager.Retry();
  }

  void FlipSpriteX()
  {
    if (!spriteRenderer)
      return;

    spriteRenderer.flipX = isMovingLeft;
  }

  // resets all character's properties to default behaviour and resets the switch
  void DeactivateAbility()
  {
    allowJump = true;
    allowMove = true;

    abilityActive = false;

    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  void Rampage()
  {
    if (isMovingLeft)
      rb.AddForce(Vector2.left  * 30);
    else
      rb.AddForce(Vector2.right * 30);
  }

  void ColdFeet()
  {
    if (!isCollidingWithGround)
    {
      DeactivateAbility();
      return;
    }

    rb.constraints =
      RigidbodyConstraints2D.FreezePositionX |
      RigidbodyConstraints2D.FreezePositionY |
      RigidbodyConstraints2D.FreezeRotation;
  }

  void Crybaby()
  {
    ShootTear(gameManager.playerManager.magmaPrefab);
  }

  void Eruption()
  {
    ShootTear(gameManager.playerManager.magmaPrefab);
  }

  void FrozenOutrage()
  {

  }

  void ShootTear(Tear tear)
  {
    Tear temp = Instantiate(tear);
    temp.transform.SetParent(gameManager.interactiblesManager.transform);
    temp.Shoot(this, gameManager.inputManager.angleToMouse, gameManager.inputManager.toMouse.normalized);
    DeactivateAbility();
  }
}