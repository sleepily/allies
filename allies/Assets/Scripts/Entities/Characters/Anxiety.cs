﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anxiety : Character
{
  public float deactivationTimestamp;
  public float deactivationTime = 4f;
  public bool isDeactivating = false;

  BoxCollider2D abilityBoxCollider;

  private void Awake()
  {
    DisableBoxCollider();
  }

  void DisableBoxCollider()
  {
    abilityBoxCollider = GetComponent<BoxCollider2D>();
    abilityBoxCollider.enabled = false;
  }

  protected override void Update()
  {
    base.Update();

    if (isDeactivating)
      UpdateDeactivationTimer();
  }

  protected override void Ability()
  {
    base.Ability();

    abilityBoxCollider.enabled = true;

    if (isJumping || !isCollidingWithGround)
      return;

    rb.isKinematic = true;
    rb.useFullKinematicContacts = true;

    rb.constraints =
      RigidbodyConstraints2D.FreezePositionX |
      RigidbodyConstraints2D.FreezePositionY |
      RigidbodyConstraints2D.FreezeRotation;
  }

  protected override void DeactivateAbility()
  {
    base.DeactivateAbility();

    abilityBoxCollider.enabled = false;

    rb.isKinematic = false;
    rb.useFullKinematicContacts = false;

    isDeactivating = false;
  }

  protected override void CheckCollisionWithEnemy(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Enemy"))
      return;

    if (abilityActive)
    {
      collision.rigidbody.velocity = Vector2.zero;
      collision.gameObject.SendMessage("Bounce");
    }

    gameManager.sceneManager.RetryLevel();
  }

  protected override void CheckCollisionWithCharacter(Collision2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    StartDeactivationTimer();
  }

  void StartDeactivationTimer()
  {
    if (!abilityActive)
      return;

    if (isDeactivating)
      return;

    isDeactivating = true;
    deactivationTimestamp = Time.time;
  }

  void UpdateDeactivationTimer()
  {
    if (Time.time < deactivationTimestamp + deactivationTime)
      return;

    DeactivateAbility();
  }
}
