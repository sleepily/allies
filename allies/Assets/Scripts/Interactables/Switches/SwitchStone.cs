using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStone : Switch
{
  public override void Init()
  {
    base.Init();
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
  }
}
