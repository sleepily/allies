using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
  public float transitionSpeed = 1f;

  public FadeCanvasGroup menuGroup;
  public FadeCanvasGroup optionsGroup;
  public FadeCanvasGroup controlsGroup;

  FadeCanvasGroup activeGroup;

  private void Start()
  {
    Init();
  }

  void Init()
  {
    activeGroup = menuGroup;

    menuGroup.FadeInstant(true);
    optionsGroup.FadeInstant(false);
    controlsGroup.FadeInstant(false);
  }

  public void MoveToMenu()
  {
    StartCoroutine(MoveToGroup(menuGroup));
  }

  public void MoveToOptions()
  {
    StartCoroutine(MoveToGroup(optionsGroup));
  }

  public void MoveToControls()
  {
    StartCoroutine(MoveToGroup(controlsGroup));
  }

  IEnumerator MoveToGroup(FadeCanvasGroup group)
  {
    if (group == activeGroup)
      yield break;

    activeGroup.FadeOut();

    yield return new WaitForSeconds(activeGroup.fadeDuration);

    group.FadeIn();

    yield return new WaitForSeconds(group.fadeDuration);

    activeGroup = group;
  }
}
