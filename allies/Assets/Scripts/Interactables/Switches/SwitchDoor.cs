using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : Interactable
{
  public override void Activate()
  {
    //TODO: implement animation
    Destroy(this.gameObject);
  }
}
