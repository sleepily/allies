using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : SubManager
{
  [Header("Emphasize menu art")]
  public float scenicTreshold = .6f;
  public bool isScenic = true;
  bool lastScenicState = true;
  bool scenicSwitchActive = false;

  [Header("Camera Properties")]
  public float scenicPosition = -8f;
  public float menuPosition = 6f;
  public float cameraSpeed = 1.2f;

  [Header("Menu Components")]
  public ParallaxParent parallaxParent;
  public FadeCanvasGroup buttonCanvasGroup;
  public Camera cameraToMove;

  private void Start()
  {
    lastScenicState = isScenic;
    ScenicTransition(true);
  }

  private void Update()
  {
    CheckInput();
    LerpCamera();
  }

  void CheckInput()
  {
    bool mouseIsInScenicView = Input.mousePosition.x < (Screen.currentResolution.width * scenicTreshold);

    SwitchToView(mouseIsInScenicView);
  }

  void LerpCamera()
  {
    if (!cameraToMove)
      cameraToMove = GameManager.globalCamera;

    cameraToMove.transform.position =
      Vector3.Lerp(cameraToMove.transform.position, CalculateNewCameraPosition(), Time.deltaTime * cameraSpeed);
  }

  void SwitchToView(bool isScenic = true)
  {
    if (scenicSwitchActive)
      return;

    if (lastScenicState == isScenic)
      return;

    scenicSwitchActive = true;

    this.isScenic = isScenic;
    lastScenicState = isScenic;

    ScenicTransition();

    scenicSwitchActive = false;
  }

  void ScenicTransition()
  {
    buttonCanvasGroup.Fade(!isScenic);
  }

  void ScenicTransition(bool isInstant = true)
  {
    if (!isInstant)
    {
      ScenicTransition();
      return;
    }

    buttonCanvasGroup.FadeInstant(!isScenic);

    cameraToMove.transform.position = CalculateNewCameraPosition();
  }

  Vector3 CalculateNewCameraPosition()
  {
    float newX = isScenic ? scenicPosition : menuPosition;

    Vector3 newPosition = (Vector3.right * newX);
    newPosition += Vector3.forward * cameraToMove.transform.position.z;

    return newPosition;
  }
}
