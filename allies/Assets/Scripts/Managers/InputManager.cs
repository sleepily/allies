using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  public GameManager gameManager;

  [Header("Direction controls")]
  public float moveX = 0f;
  public float moveY = 0f;

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

    moveX = Input.GetAxisRaw("Horizontal");
    moveY = Input.GetKeyDown(KeyCode.W) ? 1f : 0f;
  }
}