using UnityEngine;

public class Character : Entity
{  
  [Header("Spawn Options")]
  public bool startWithAbility = false;
  public bool isMovingLeft = false;

  [Header("Physics")]
  public Rigidbody2D rb;
  public Vector2 velocity;
  public bool isCollidingWithGround;
  public bool isCollidingWithWall;
  public PhysicsMaterial2D moveMaterial;
  public PhysicsMaterial2D idleMaterial;
  public bool isJumping;

  public bool canUseAbilityInAir;
  public bool allowJump;
  public bool allowMove;

  [Header("Animations")]
  public SpriteRenderer spriteRenderer;
  public Animator animator;
  private float horizontalVelocityAbs, verticalVelocityAbs;
  public bool isMoving;
  public bool abilityActive;
  public int abilityIndex = 0;
  public float angle;
  private float angleVelocityThreshold = 1f;

  public SpriteGlow.SpriteGlowEffect spriteGlowEffect;
  float spriteGlowTransitionSpeed = 6f;
  Color spriteGlowColor;

  public override void Init()
  {
    // base init without bool initialized
    gameManager = GameManager.globalGameManager;
    MoveToParentTransform();

    GetAllComponents();
    InitSpriteGlow();
    DeactivateAbility();

    if (startWithAbility)
      abilityActive = true;

    initialized = true;
  }

  public override void MoveToParentTransform()
  {
    transform.SetParent(gameManager.characterManager.transform);
  }

  protected virtual void GetAllComponents()
  {
    if (!rb)
      rb = GetComponent<Rigidbody2D>();

    if (!animator)
      animator = GetComponent<Animator>();

    if (!spriteRenderer)
      spriteRenderer = GetComponent<SpriteRenderer>();

    if (!spriteGlowEffect)
      spriteGlowEffect = GetComponent<SpriteGlow.SpriteGlowEffect>();
  }
  
  void InitSpriteGlow()
  {
    spriteGlowColor = spriteGlowEffect.GlowColor;

    spriteGlowColor.a = 0f; // reset alpha so nothing shines through on level load/retry

    spriteGlowEffect.GlowColor = spriteGlowColor;
  }

  protected virtual void Update()
  {
    if (!initialized)
      return;

    UpdateCharacterGlow();
    CheckMovementDirection();
    CheckAbilityStatus();
    SetAnimatorProperties();
  }

  public bool IsActiveCharacter()
  {
    if (!gameManager.characterManager.activeCharacter)
      return false;

    return this == gameManager.characterManager.activeCharacter;
  }

  void UpdateCharacterGlow()
  {
    SetCharacterGlow(IsActiveCharacter());
  }

  void SetCharacterGlow(bool enabled)
  {
    float alpha = enabled ? 1f : 0f;

    Color glowColor = spriteGlowEffect.GlowColor;
    glowColor.a = Mathf.Lerp(glowColor.a, alpha, Time.deltaTime * spriteGlowTransitionSpeed);

    spriteGlowEffect.GlowColor = glowColor;
  }

  private void FixedUpdate()
  {
    if (!initialized)
      return;

    GetGlobalGravityScale();
    rb.sharedMaterial = idleMaterial;

    CheckForCharacterDistance();
  }

  protected virtual void CheckForCharacterDistance()
  {
    if (!initialized)
      return;

    if (gameManager.characterManager.charactersInLevel.Count == 1)
      return;

    if (!abilityActive)
      return;

    foreach (Character character in gameManager.characterManager.GetActiveCharactersAsList())
    {
      if (character == this)
        continue;

      float distance = Vector2.Distance(this.transform.position, character.transform.position);

      if (distance < 2f)
      {
        // Debug.Log(string.Format("Disabled {0}'s ability due to distance to {1}.", this.name, character.name));
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
    if (isCollidingWithGround)
      return;

    // check for collision in the lower 25% of the collider in order to enable jumping
    foreach (ContactPoint2D cp in collision.contacts)
      if (cp.point.y < transform.position.y + (Vector2.down * .75f).y)
      {
        isCollidingWithGround = true;
        isCollidingWithWall = false;
        isJumping = false;
        
        SoundManager.PlayOneShot(SoundManager.soundManager.character_land, this);

        return;
      }
  }

  protected virtual void CheckCollisionWithEnemy(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Enemy"))
      return;

    gameManager.sceneManager.RetryLevelOnKill();
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
    rb.gravityScale = GameManager.globalGravityScale;
  }

  private void SetAnimatorProperties()
  {
    if (animator == null)
      return;

    CharacterSpecificAnimationProperties();
    GetAbsoluteVelocityValues();
    PreventIdleAnimationWhileJumping();
    SetAnimatorVariables();
    MirrorSpriteIfMovingLeft();
  }

  protected virtual void SetAnimatorVariables()
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

  public virtual void ActivateAbility()
  {
    if (!canUseAbilityInAir)
      if (!isCollidingWithGround)
        return;

    abilityActive = true;
    gameManager.characterManager.SetNextCharacterAsActive();
  }

  protected void CheckAbilityStatus()
  {
    if (!abilityActive)
      return;

    Ability();
  }

  public void Move(float axisHorizontal)
  {
    if (!allowMove)
      return;

    if (isCollidingWithWall)
      return;

    rb.sharedMaterial = moveMaterial;

    velocity.x = axisHorizontal;

    float moveForce = velocity.x * rb.gravityScale * gameManager.characterManager.movementForce;

    rb.velocity = new Vector2(moveForce, rb.velocity.y);
  }

  public void Jump(float axisVertical)
  {
    if (!allowJump)
      return;

    if (isJumping)
      return;

    if (!isCollidingWithGround)
      return;

    if (axisVertical < .5f)
      return;
    
    isJumping = true;

    rb.sharedMaterial = moveMaterial;

    velocity.y = axisVertical;

    float jumpForce = velocity.y * rb.gravityScale * gameManager.characterManager.jumpForce;

    rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    isCollidingWithGround = false;

    SoundManager.PlayOneShot(SoundManager.soundManager.character_jump, this);
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
  
  public virtual void DeactivateAbility()
  {
    if (!initialized)
      return;

    allowJump = true;
    allowMove = true;

    abilityActive = false;

    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
  }

  protected virtual void ShootTear(Tear tearPrefab)
  {
    Tear tear = Instantiate(tearPrefab);
    tear.Shoot(this, true);
  }
}