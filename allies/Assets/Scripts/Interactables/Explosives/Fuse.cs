using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : Interactable
{
  public override void Init()
  {
    base.Init();
    polygonCollider2D.isTrigger = true;
  }

  public override void Activate()
  {
    base.Activate();


  }
}
