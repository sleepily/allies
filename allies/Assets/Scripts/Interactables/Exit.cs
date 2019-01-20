using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactable
{
  public bool activated = false;
  public List<Character> entered;
  public int charactersMissing = 3;
  bool gotCharacterCount = false;

	void Start ()
  {
    Init();
    SetColliderToTrigger();
	}

  void GetMissingCharacterCount()
  {
    if (gotCharacterCount)
      return;

    charactersMissing = gameManager.playerManager.activeCharactersInLevel.Count;

    if (charactersMissing == 0)
      return;

    gotCharacterCount = true;
  }

  private void FixedUpdate()
  {
    GetMissingCharacterCount();
    CheckForAllCharacters();
  }

  void CheckForAllCharacters()
  {
    if (!gotCharacterCount)
      return;

    if (charactersMissing == 0)
      ExitLevel();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckCharacterTrigger(collision);
  }

  void SetColliderToTrigger()
  {
    polygonCollider2D.isTrigger = true;
  }

  void CheckCharacterTrigger(Collider2D collision)
  {
    if (!collision.CompareTag("Character"))
      return;

    Character c = collision.gameObject.GetComponent<Character>();
    HideCharacter(c);
  }

  void HideCharacter(Character character)
  {
    charactersMissing--;

    character.gameObject.SetActive(false);
    character.enabled = false;
  }

  void ExitLevel()
  {
    if (activated)
      return;

    activated = true;
    gameManager.sceneManager.LoadNextLevel();
  }
}
