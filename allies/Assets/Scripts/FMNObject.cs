using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Forget Me Not Object
 * 
 * Base class for every entity and interactable in the game.
 */

public class FMNObject : MonoBehaviour
{
  [HideInInspector]
  public GameManager gameManager;

  [HideInInspector]
  public bool initialized = false;

  public AudioSource audioSource;

  private void Awake()
  {
    gameManager = GameManager.globalGameManager;
  }

  public virtual void Init()
  {
    gameManager = GameManager.globalGameManager;
    MoveToParentTransform();
    InitAudioSource();
    // Debug.Log("FMN: Initializing " + gameObject.name);
    initialized = true;
  }

  public virtual void MoveToParentTransform()
  {
    // don't move anywhere if object is not specialized
    return;
  }

  protected virtual void InitAudioSource()
  {
    audioSource = gameObject.AddComponent<AudioSource>();
    audioSource.outputAudioMixerGroup = gameManager.soundManager.mixer_master;
  }

  protected virtual void SetAnimatorTrigger(Animator animator, string trigger)
  {
    if (!animator)
    {
      Debug.LogError(string.Format("Cannot set animation trigger: Animation \"{0}\" is null.", animator.name));
      return;
    }

    if (!animator.gameObject.activeSelf)
    {
      Debug.LogError(string.Format("Setting {0} active to set Animator \"{1}\".", animator.gameObject.name, animator.name));
      animator.gameObject.SetActive(true);
    }

    if (!animator.isInitialized)
    {
      Debug.LogError(string.Format("Setting {0} active to set Animator \"{1}\".", animator.gameObject.name, animator.name));
      animator.Rebind();
    }

    animator.SetTrigger(trigger);
  }

  protected virtual void SetAnimatorTrigger(Animator animator, string trigger, bool reset = false)
  {
    if (!reset)
      animator.SetTrigger(trigger);
    else
      animator.ResetTrigger(trigger);
  }
}
