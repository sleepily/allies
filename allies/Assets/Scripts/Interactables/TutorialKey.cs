using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKey : Tutorial
{
  public enum Key
  {
    arrowDown,
    arrowLeft,
    arrowRight,
    arrowUp,
    a,
    d,
    e,
    f,
    q,
    r,
    s,
    w
  }

  public Key key;

  public List<Sprite> sprites;

  SpriteRenderer spriteRenderer;

  private void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = sprites[(int)key];
  }
}
