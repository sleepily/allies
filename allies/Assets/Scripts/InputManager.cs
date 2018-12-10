using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  public GameManager gm;

  [Header("Player related")]
  public bool switchAction  = false;
  public bool groupAction   = false;
  public bool abilityAction = false;

  [Header("Scene related")]
  public bool reloadScene   = false;
  public bool cameraSwitch  = false;
  
  void Update()
  {
    switchAction  = Input.GetKeyDown(KeyCode.R);
    groupAction   = Input.GetKeyDown(KeyCode.F);
    abilityAction = Input.GetKeyDown(KeyCode.E);

    cameraSwitch  = Input.GetKeyDown(KeyCode.Space);
    reloadScene   = Input.GetKeyDown(KeyCode.Q);
  }
}