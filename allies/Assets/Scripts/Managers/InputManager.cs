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
    UpdateFunctionKeys();
    UpdateMovementKeys();

    CalculateAngleBetweenPlayerAndMouse();
  }

  void UpdateMovementKeys()
  {
    moveX = Input.GetAxisRaw("Horizontal");
    moveY = (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow)) ? 1f : 0f;
  }

  void UpdateFunctionKeys()
  {
    switchAction = Input.GetKeyDown(KeyCode.R);
    groupAction = Input.GetKeyDown(KeyCode.F);
    abilityAction = Input.GetKeyDown(KeyCode.E);

    cameraSwitch = Input.GetKeyDown(KeyCode.Space);
    reloadScene = Input.GetKeyDown(KeyCode.Q);
  }

  void CalculateAngleBetweenPlayerAndMouse()
  {
    if (!gameManager.playerManager.activeCharacter)
      return;

    Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 character = gameManager.playerManager.activeCharacter.transform.position;
    Vector2 toMouse = mouse - character;
    float angleToMouse = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;

    if (angleToMouse < 0)
      angleToMouse += 360;
  }
}