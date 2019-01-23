using System.Collections;
using UnityEngine;

public class InputManager : SubManager
{
  [Header("Direction controls")]
  public float moveX = 0f;
  public float moveY = 0f;

  [Header("Player related")]
  public bool switchAction = false;
  public bool fusionAction, abilityAction, abilityAction2, backAction = false;

  [Header("Tear related")]
  public Vector2 toMouse     = Vector2.zero;
  public float angleToMouse  = 0f;

  [Header("Scene related")]
  public bool reloadScene   = false;
  public bool cameraSwitch  = false;

  public override void Init()
  {
    base.Init();
  }

  void Update()
  {
    UpdateFunctionKeys();
    UpdateMovementKeys();

    if (backAction)
      gameManager.sceneManager.LoadScreen(SceneManager.Screen.mainMenu);

    CalculateAngleBetweenPlayerAndMouse();
  }

  void UpdateMovementKeys()
  {
    moveX = Input.GetAxisRaw("Horizontal");
    if (moveX < -.1) moveX = -1;
    if (moveX >  .1) moveX =  1;
    
    moveY = Input.GetAxisRaw("Vertical");
    if (moveY < 0)  moveY = 0;
    if (moveY > 0)  moveY = 1;
  }

  void UpdateFunctionKeys()
  {
    switchAction   = Input.GetKeyDown(KeyCode.Q);
    fusionAction   = Input.GetKeyDown(KeyCode.F);
    abilityAction  = Input.GetKeyDown(KeyCode.E);
    abilityAction2 = Input.GetKeyDown(KeyCode.S);
    backAction     = Input.GetKeyDown(KeyCode.Escape);
    
    reloadScene   = Input.GetKeyDown(KeyCode.R);
  }

  void CalculateAngleBetweenPlayerAndMouse()
  {
    if (!gameManager)
      return;

    if (!gameManager.playerManager.activeCharacter)
      return;

    Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 character = gameManager.playerManager.activeCharacter.transform.position;
    toMouse = mouse - character;
    angleToMouse = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;

    if (angleToMouse < 0)
      angleToMouse += 360;
  }
}