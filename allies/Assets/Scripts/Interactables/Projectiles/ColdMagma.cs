using System.Collections.Generic;
using UnityEngine;

public class ColdMagma : Interactable
{
  public SpriteRenderer spriteRenderer;
  public List<Sprite> sprites;

  public void Init(MagmaTear tear)
  {
    gameManager = tear.gameManager;

    base.Init();
    GetSpriteRenderer();

    transform.rotation = Quaternion.Euler(0, 0, tear.angle);
    transform.position = tear.transform.position;

    SetSprite(tear.spriteIndex);
  }

  private void GetSpriteRenderer()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void SetSprite(int spriteIndex)
  {
    spriteRenderer.sprite = sprites[spriteIndex];
  }
}