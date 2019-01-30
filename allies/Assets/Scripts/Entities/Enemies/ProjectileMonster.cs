﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMonster : Enemy
{
  [Header("Shooting")]
  public Projectile projectilePrefab;
  public float interval = .3f;

  float time;

  public override void Init()
  {
    base.Init();

    SetTime();
  }

  private void Update()
  {
    CheckShoot();
  }

  void SetTime()
  {
    time = Time.time;
  }

  void CheckShoot()
  {
    if (!ShootingAvailable())
      return;

    Projectile projectile = Instantiate(projectilePrefab);
    projectile.transform.SetParent(this.transform);
    projectile.Shoot(this, Vector2.left * this.transform.localScale.x); //turn this around
  }

  bool ShootingAvailable()
  {
    if (Time.time < time + interval)
      return false;

    time += interval;
    return true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    KillOnProjectileCollision(collision);
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    KillOnProjectileCollision(collision);
  }

  protected void KillOnProjectileCollision(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Projectile"))
      return;

    Destroy(this.gameObject);
  }
}
