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

  // TODO: make this work with list of characters
  void SpawnCharacters()
  {
    if (gameManager.designManager.ragPlaceholder.activeSelf)
    {
      rage = Instantiate(ragePrefab);
      rage.name = ragePrefab.name;
      rage.playerManager = this;
      rage.transform.SetParent(this.transform);
      rage.transform.position += gameManager.designManager.ragPlaceholder.transform.position;

      characters.Add(rage);
    }

    if (gameManager.designManager.depPlaceholder.activeSelf)
    {
      depression = Instantiate(depressionPrefab);
      depression.name = depressionPrefab.name;
      depression.playerManager = this;
      depression.transform.SetParent(this.transform);
      depression.transform.position += gameManager.designManager.depPlaceholder.transform.position;

      characters.Add(depression);
    }

    if (gameManager.designManager.anxPlaceholder.activeSelf)
    {
      anxiety = Instantiate(anxietyPrefab);
      anxiety.name = anxietyPrefab.name;
      anxiety.playerManager = this;
      anxiety.transform.SetParent(this.transform);
      anxiety.transform.position += gameManager.designManager.anxPlaceholder.transform.position;

      characters.Add(anxiety);
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
