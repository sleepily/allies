using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousFlower : FireFlower
{
  public override void Init()
  {
    base.Init();

    Activate();
  }
  
  public override void Deactivate()
  {
    base.Deactivate();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    CheckCharacterCollision(collision);
  }

  void CheckCharacterCollision(Collision2D collision)
  {
    if (!actionActivated)
      return;

    if (!collision.gameObject.CompareTag("Character"))
      return;

    gameManager.sceneManager.RetryLevel();
  }
}
