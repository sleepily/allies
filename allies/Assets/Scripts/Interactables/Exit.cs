using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactable
{
  public List<Character> entered;

	void Start ()
  {
    Init();
	}
  
  void ActivateWhenNoCharactersAreMissing()
  {
    if (actionActivated)
      return;

    List<Character> missingCharacters = gameManager.characterManager.GetActiveCharactersAsList();
    string missing = "";

    if (missingCharacters.Count > 0)
    {
      foreach (Character missingCharacter in missingCharacters)
        missing += missingCharacter.name + ", ";

      // Debug.Log("missing characters: " + missingCharacters.Count + "; " + missing);
      return;
    }

    Activate();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CheckCharacterTrigger(collision);
  }

  void CheckCharacterTrigger(Collider2D collision)
  {
    if (!collision.CompareTag("Character"))
      return;

    Character c = collision.gameObject.GetComponent<Character>();
    HideCharacter(c);

    ActivateWhenNoCharactersAreMissing();
  }

  void HideCharacter(Character character)
  {
    // Debug.Log("hiding character " + character.name);
    character.gameObject.SetActive(false);
    character.enabled = false;
  }

  public override void Activate()
  {
    if (actionActivated)
      return;

    base.Activate();
    ExitLevel();
  }

  void ExitLevel()
  {
    // Debug.Log("Exiting level " + gameManager.sceneManager.levelID);
    gameManager.sceneManager.FinishLevel();
  }
}
