using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMenuContents : MonoBehaviour
{
  public float transitionSpeed = .6f;

  public FadeCanvasGroup menuGroup;
  public FadeCanvasGroup optionsGroup;

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
  }

  public void MoveToMenu()
  {
    StartCoroutine(MoveToGroup(menuGroup));
  }

  public void MoveToOptions()
  {
    StartCoroutine(MoveToGroup(optionsGroup));
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
