using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : SubManager
{
  [Header("Character Prefabs")]
  public Rage ragePrefab;
  public Anxiety anxietyPrefab;
  public Depression depressionPrefab;
  List<Character> characterPrefabs = new List<Character>();

  public List<Character> activeCharactersInLevel = new List<Character>();
  public int activeCharacterIndex = 0;
  public Character activeCharacter;

  [Header("Group/Team")]
  public float maxGroupingDistance = 3f;

  [Header("Global Character Physics")]
  public float movementForce = 2f;
  public float jumpForce = 2f;
  public float globalGravityScale = 5f;
  
  public override void Init()
  {
    base.Init();

    CreateCharacterPrefabList();
    SpawnCharacters();
    SetPlayerManagerParent();
    SetActiveCharacter();
  }

  void Update ()
  {
    GetInput();
    MoveActiveCharacter();
	}

  private void FixedUpdate()
  {

  }

  void CreateCharacterPrefabList()
  {
    characterPrefabs.Add(ragePrefab);
    characterPrefabs.Add(anxietyPrefab);
    characterPrefabs.Add(depressionPrefab);
  }
  
  void SpawnCharacters()
  {
    int index = 0;

    foreach (CharacterPlaceholder placeholder in gameManager.designManager.characterPlaceholders)
    {
      if (placeholder.gameObject.activeSelf)
      {
        Character characterPrefabToSpawn = null;

        foreach (Character prefab in characterPrefabs)
          if (placeholder.name == prefab.name)
            characterPrefabToSpawn = prefab;

        if (!characterPrefabToSpawn)
        {
          Debug.LogError("Couldn't assign character prefab for placeholder \"" + placeholder.name + "\".");
          continue;
        }

        activeCharactersInLevel.Add(Instantiate(characterPrefabToSpawn));
        
        activeCharactersInLevel[index].name = characterPrefabToSpawn.name;
        activeCharactersInLevel[index].gameManager = gameManager;
        activeCharactersInLevel[index].transform.SetParent(this.transform);
        activeCharactersInLevel[index].transform.position += placeholder.transform.position;

        index++;
      }
    }
  }

  void SetPlayerManagerParent()
  {
    foreach (Character c in activeCharactersInLevel)
    {
      if (c == null)
        break;

      c.playerManager = this;
    }
  }

  void SetActiveCharacter()
  {
    activeCharacter = activeCharactersInLevel[activeCharacterIndex];
  }

  void SetNextCharacterAsActive()
  {
    if (activeCharacterIndex + 1 >= activeCharactersInLevel.Count)
      activeCharacterIndex = 0;
    else
      activeCharacterIndex++;

    SetActiveCharacter();
  }

  void GetInput()
  {
    if (activeCharacter == null || activeCharacter.rb == null)
      return;

    if (gameManager.inputManager.switchAction)
      SetNextCharacterAsActive();

    if (gameManager.inputManager.reloadScene)
      gameManager.levelManager.Retry();

    if (gameManager.inputManager.fusionAction)
    {
      CombineCharacters();
    }

    if (gameManager.inputManager.abilityAction)
    {
      activeCharacter.abilityIndex = 0;
      activeCharacter.ActivateAbility();
    }

    if (gameManager.inputManager.abilityAction2)
      if (activeCharacter.name == "Depression")
      {
        Depression dep = activeCharacter.GetComponent<Depression>();
        if (dep.jetpackActivated)
          return;

        activeCharacter.abilityIndex = 1;
        activeCharacter.ActivateAbility();
      }
  }

  void MoveActiveCharacter()
  {
    activeCharacter.Move(gameManager.inputManager.moveX);
    activeCharacter.Jump(gameManager.inputManager.moveY);
  }

  void CombineCharacters()
  {
    if (activeCharactersInLevel.Count == 1)
      return;
    
    foreach (Character character in activeCharactersInLevel)
    {
      if (character == activeCharacter)
        return;

      float distance = Vector2.Distance(activeCharacter.transform.position, character.transform.position);

      if (distance > maxGroupingDistance)
        continue;



      // grab distance
      // group if nearest character < groupingdistance
    }
  }
}
