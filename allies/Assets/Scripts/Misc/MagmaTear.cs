﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaTear : Tear
{
  [Header("After Collision")]
  public ColdMagma coldMagmaPrefab;

  override protected void Collide(Collision2D collision)
  {
    isColliding = true;

    SpawnColdMagma();

    Destroy(this.gameObject);
  }
  
  void SpawnColdMagma()
  {
    ColdMagma cold = Instantiate(coldMagmaPrefab);
    cold.Init(this);
  }
}