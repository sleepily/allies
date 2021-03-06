﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFlower : FireFlower
{
  [Header("Shooting")]
  public Projectile projectilePrefab;
  public float interval = .3f;

  float timestamp_lastShot = -1f;

  public override void Init()
  {
    gameManager = GameManager.globalGameManager;
    MoveToParentTransform();

    SetTime();
    initialized = true;
  }

  private void Update()
  {
    CheckShoot();
  }

  void SetTime()
  {
    timestamp_lastShot = Time.time;
  }

  void CheckShoot()
  {
    if (!ShootingAvailable())
      return;

    Projectile projectile = Instantiate(projectilePrefab);
    projectile.transform.SetParent(this.transform);
    projectile.Shoot(this, Vector2.up); //turn this around
  }

  bool ShootingAvailable()
  {
    // enemy has not been initialized yet
    if (timestamp_lastShot < 0)
      return false;

    if (Time.time < timestamp_lastShot + interval)
      return false;

    timestamp_lastShot += interval;
    return true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckCharacterCollision(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    CheckCharacterCollision(collision);
  }

  void CheckCharacterCollision(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    gameManager.sceneManager.RetryLevelOnKill();
  }
}
