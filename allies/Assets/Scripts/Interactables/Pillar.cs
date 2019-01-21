using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : Interactable
{
  public PhysicsMaterial2D icePhysicsMaterial;

  public override void Init()
  {
    base.Init();
    rb.sharedMaterial = icePhysicsMaterial;
  }
}
