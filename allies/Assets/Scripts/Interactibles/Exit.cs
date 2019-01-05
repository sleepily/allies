using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactible
{
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
    ExitLevel();
  }

  void ExitLevel()
  {
    Debug.Log("Finished Level");
    gameManager.ChangeState(GameManager.State.cutscene, gameObject);
  }
}
