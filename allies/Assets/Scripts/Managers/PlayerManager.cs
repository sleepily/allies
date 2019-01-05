using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
  public GameManager gameManager;

  [Header("Character Prefabs")]
  public Character ragePrefab;
  public Character anxietyPrefab;
  public Character depressionPrefab;

  [Header("Active/Controllable Characters")]
  public Character rage;
  public Character anxiety;
  public Character depression;

  public List<Character> characters = new List<Character>();
  public int characterIndex = 0;
  public Character activeCharacter;

  [Header("Group/Team")]
  public List<Character> allies = new List<Character>();
  public float maxGroupingDistance = 3f;

  [Header("Global Character Physics")]
  public float movementForce = 2f;
  public float jumpForce = 2f;
  public float globalGravityScale = 5f;
  
	void Start ()
  {
    SpawnCharacters();
    SetPlayerManagerParent();
    SetActiveCharacter();
  }

  void Update ()
  {
    GetInput();
    CalculateMovement();
	}

  private void FixedUpdate()
  {
    CheckGroupability();
  }

  private void CheckGroupability()
  {
    allies.Clear();

    string allyString = "ALLIES:\n\n";

    foreach (Character otherCharacter in characters)
    {
      if (otherCharacter == null)
        break;

      if (maxGroupingDistance > Vector2.Distance(activeCharacter.transform.position, otherCharacter.transform.position))
      {
        Debug.DrawRay(otherCharacter.transform.position, Vector2.up, Color.cyan);
        allies.Add(otherCharacter);
        allyString += otherCharacter.name + "\n";
      }
    }

    gameManager.uiManager.alliesText.text = allyString;
  }
  
  void SpawnCharacters()
  {
    int index = 0;

    foreach (CharacterPlaceholder placeholder in gameManager.designManager.characterPlaceholders)
    {
      if (placeholder.gameObject.activeSelf)
      {
        characters.Add(Instantiate(placeholder.characterPrefab));
        
        characters[index].name = placeholder.characterPrefab.name;
        characters[index].playerManager = this;
        characters[index].transform.SetParent(this.transform);
        characters[index].transform.position += placeholder.transform.position;
        
        // connect r/a/d in list and variables
        switch (characters[index].name)
        {
          case "Rage":
            rage = characters[index];
            break;
          case "Anxiety":
            anxiety = characters[index];
            break;
          case "Depression":
            depression = characters[index];
            break;
          default:
            Debug.LogError("Bad temporary character.");
            break;
        }

        index++;
      }
    }
  }

  void SetPlayerManagerParent()
  {
    foreach (Character c in characters)
    {
      if (c == null)
        break;

      c.playerManager = this;
    }
  }

  void SetActiveCharacter()
  {
    activeCharacter = characters[characterIndex];
  }

  void SetNextCharacterAsActive()
  {
    if (characterIndex + 1 >= characters.Count)
      characterIndex = 0;
    else
      characterIndex++;

    SetActiveCharacter();
  }

  void GetInput()
  {
    if (activeCharacter == null || activeCharacter.rb == null)
      return;

    if (gameManager.inputManager.switchAction)
      SetNextCharacterAsActive();

    if (gameManager.inputManager.reloadScene)
      SceneManager.LoadScene(0);

    if (gameManager.inputManager.abilityAction)
      activeCharacter.state = Character.State.ability;
  }

  void CalculateMovement()
  {
    float moveX = gameManager.inputManager.moveX * movementForce * Time.deltaTime;
    Vector3 horizontalForce = Vector3.right * moveX * movementForce;

    float moveY = Mathf.Clamp01(gameManager.inputManager.moveY);
    Vector3 verticalForce = Vector3.up * jumpForce * moveY;

    foreach (Character ally in allies)
    {
      ally.CheckAbilityStatus();
      ally.Move(horizontalForce);
      ally.Jump(verticalForce);
    }
  }
}
