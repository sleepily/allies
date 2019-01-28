using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : SubManager
{
  [Header("Character Prefabs")]
  public Rage ragePrefab;
  public Anxiety anxietyPrefab;
  public Depression depressionPrefab;
  public Stress stressPrefab;
  public Frustration frustrationPrefab;
  public Apathy apathyPrefab;
  List<Character> characterPrefabs = new List<Character>();

  [Header("Instantiated Characters")]
  public List<Character> charactersInLevel = new List<Character>();
  public List<CombinedCharacter> combinedCharacters = new List<CombinedCharacter>();
  public Stress stress;
  public Frustration frustration;
  public Apathy apathy;
  public int activeCharacterIndex = 0;
  public Character activeCharacter;
  public CombinedCharacter activeCombinedCharacter;

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

  void Update()
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

  void SpawnFusedCharacter(Character initiator, Character determinator)
  {
    string a = initiator.name;
    string b = determinator.name;

    bool flag_rage, flag_depression, flag_anxiety = false;
    
    flag_rage       = (a == "Rage" || b == "Rage");
    flag_depression = (a == "Depression" || b == "Depression");
    flag_anxiety    = (a == "Anxiety" || b == "Anxiety");

    if (flag_rage && flag_anxiety)
      activeCombinedCharacter = stress;

    if (flag_anxiety && flag_depression)
      activeCombinedCharacter = apathy;

    if (flag_depression && flag_rage)
      activeCombinedCharacter = frustration;

    if (!activeCombinedCharacter)
    {
      Debug.Log("Couldn't fuse characters.");
      return;
    }

    activeCombinedCharacter.Fusion(initiator, determinator);
    Debug.Log("Set " + activeCombinedCharacter.name + " as active CC.");
  }

  void SpawnCharacters()
  {
    foreach (CharacterPlaceholder placeholder in gameManager.designManager.characterPlaceholders)
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

      Character spawn = Instantiate(characterPrefabToSpawn);
      charactersInLevel.Add(spawn);

      spawn.name = characterPrefabToSpawn.name;
      spawn.transform.position += placeholder.transform.position;
      spawn.startWithAbility = placeholder.startWithAbility;
      spawn.gameObject.SetActive(placeholder.gameObject.activeSelf);
    }

    apathy = Instantiate(apathyPrefab);
    stress = Instantiate(stressPrefab);
    frustration = Instantiate(frustrationPrefab);

    charactersInLevel.Add(apathy);
    charactersInLevel.Add(stress);
    charactersInLevel.Add(frustration);
    
    combinedCharacters.Add(apathy);
    combinedCharacters.Add(stress);
    combinedCharacters.Add(frustration);

    foreach (CombinedCharacter cc in combinedCharacters)
    {
      cc.gameObject.SetActive(false);
      cc.transform.SetParent(gameManager.characterManager.transform);
    }
  }

  void SetPlayerManagerParent()
  {
    foreach (Character c in charactersInLevel)
    {
      if (c == null)
        break;

      c.characterManager = this;
    }
  }

  public void SetActiveCharacter(Character character)
  {
    if (!character.gameObject.activeSelf)
    {
      Debug.Log("Character is not active.");
      return;
    }

    activeCharacterIndex = charactersInLevel.IndexOf(character);

    activeCharacter = character;
  }

  void SetActiveCharacter()
  {
    Character temp = charactersInLevel[activeCharacterIndex];

    if (!temp.gameObject.activeSelf)
    {
      SetNextCharacterAsActive();
      return;
    }

    activeCharacter = charactersInLevel[activeCharacterIndex];
  }

  public void SetNextCharacterAsActive()
  {
    if (activeCharacterIndex + 1 >= charactersInLevel.Count)
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
      CombineCharacters();

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
    if (!activeCharacter)
      return;

    if (!activeCharacter.gameObject.activeSelf)
      SetNextCharacterAsActive();

    activeCharacter.Move(gameManager.inputManager.moveX);
    activeCharacter.Jump(gameManager.inputManager.moveY);
  }

  public List<Character> GetActiveCharactersAsList()
  {
    List<Character> activeCharacters = new List<Character>();

    foreach (Character character in charactersInLevel)
      if (character.gameObject.activeSelf)
        activeCharacters.Add(character);

    return activeCharacters;
  }

  void CombineCharacters()
  {
    List<Character> activeCharactersInLevel = GetActiveCharactersAsList();

    if (activeCharactersInLevel.Count <= 1)
    {
      Debug.Log("Only 1 Character in level.");
      return;
    }

    Debug.Log("Checking for nearest Character.");

    Character nearest = null;
    float minDistance = maxGroupingDistance;

    foreach (Character character in activeCharactersInLevel)
    {
      if (character == activeCharacter)
      {
        Debug.Log("Checking from Character " + character.name + ".");
        continue;
      }

      float distance = Vector2.Distance(activeCharacter.transform.position, character.transform.position);

      if (distance > minDistance)
      {
        Debug.Log("Character " + character.name + " not close enough.");
        continue;
      }

      minDistance = distance;
      nearest = character;
    }

    if (!nearest)
    {
      Debug.Log("No character is close enough.");
      return;
    }

    SpawnFusedCharacter(activeCharacter, nearest);
  }
}
