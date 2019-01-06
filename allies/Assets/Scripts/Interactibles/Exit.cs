using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactible
{
  public bool activated = false;

	void Start ()
  {
    Init();
    ModifyCollider();
	}

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckCharacterCount(collision);
  }

  void ModifyCollider()
  {
    polygonCollider2D.isTrigger = true;
  }

  void CheckCharacterCount(Collider2D collision)
  {
    if (collision.CompareTag("Character"))
      ExitLevel();
  }

  void ExitLevel()
  {
    if (activated)
      return;

    activated = true;
    Debug.Log("Finished Level");
    gameManager.levelManager.LoadNextLevel();
  }
}
