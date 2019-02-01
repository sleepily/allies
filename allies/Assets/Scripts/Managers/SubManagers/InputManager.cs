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
  [SerializeField]
  Vector2 toMouse = Vector2.zero;
  [SerializeField]
  float angleToMouse = 0f;

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

  public Vector2 CalculateVectorBetweenCharacterAndMouse(Character character)
  {
    if (!gameManager)
      return Vector2.zero;

    if (!character)
      return Vector2.zero;

    Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 characterPosition = character.transform.position;
    toMouse = mouse - characterPosition;

    return toMouse;
  }

  public float CalculateAngleToMouse()
  {
    angleToMouse = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;

    if (angleToMouse < 0)
      angleToMouse += 360;

    return angleToMouse;
  }
}