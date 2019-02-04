using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : SubManager
{
  [Header("Emphasize menu art")]
  public bool isScenic = true;
  bool scenicSwitchActive = false;

  [Header("Menu Components")]
  public ParallaxParent parallaxParent;
  public FadeCanvasGroup buttonCanvasGroup;
  public Camera cameraToMove;

  private void Start()
  {
    buttonCanvasGroup.FadeInstant(!isScenic);
  }

  private void Update()
  {
    CheckInput();
  }

  void CheckInput()
  {
    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
      ScenicSwitch();
  }

  void ScenicSwitch()
  {
    if (scenicSwitchActive)
      return;

    scenicSwitchActive = true;

    isScenic = !isScenic;

    ScenicTransition();

    scenicSwitchActive = false;
  }

  void ScenicTransition()
  {
    buttonCanvasGroup.Fade(!isScenic);

    Vector3 cameraPosition = cameraToMove.transform.position;
    float newX = isScenic ? -8 : 0;
    cameraPosition.x = newX;
    cameraToMove.transform.position = cameraPosition;
  }
}
