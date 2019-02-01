﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : Interactable
{
  public FireFlowerFlame flame;

  public override void Activate()
  {
    base.Activate();
    flame.Activate();
  }

  public override void Deactivate()
  {
    base.Deactivate();
    flame.Deactivate();
  }
}
