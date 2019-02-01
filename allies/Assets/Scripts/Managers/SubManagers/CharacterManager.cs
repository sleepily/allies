using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : SubManager
{
  [Header("Character Prefabs")]
  public Stress stressPrefab;
  public Frustration frustrationPrefab;
  public Apathy apathyPrefab;
  List<Character> characterPrefabs = new List<Character>();

  [Header("Instantiated Characters")]
  public List<Character> charactersInLevel = new List<Character>();
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

  public override void Init()
  {
    base.Init();
    
    FindCharactersInLevel();
    SetActiveCharacter(charactersInLevel[0]);
  }

  void Update()
  {
    GetInput();
    MoveActiveCharacter();
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
      Debug.Log("Couldn't set fusion flags. Check Character names!");
      return;
    }

    activeCombinedCharacter.Fusion(initiator, determinator);
    // Debug.Log("Set " + activeCombinedCharacter.name + " as active CC.");
  }

  void FindCharactersInLevel()
  {
    foreach (Character character in FindObjectsOfType<Character>())
    {
      character.Init();
      charactersInLevel.Add(character);
      // Debug.Log("Found character: " + character.name);
    }

    apathy = Instantiate(apathyPrefab);
    stress = Instantiate(stressPrefab);
    frustration = Instantiate(frustrationPrefab);

    charactersInLevel.Add(apathy);
    charactersInLevel.Add(stress);
    charactersInLevel.Add(frustration);
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

    int maxTries = 6;
    while (!temp.gameObject.activeSelf)
    {
      if (maxTries < 0)
        break;

      IncreaseActiveCharacterIndex();
      maxTries--;
    }

    activeCharacter = charactersInLevel[activeCharacterIndex];
  }

  public void SetNextCharacterAsActive()
  {
    IncreaseActiveCharacterIndex();

    SetActiveCharacter();
  }

  void IncreaseActiveCharacterIndex()
  {
    if (activeCharacterIndex + 1 >= charactersInLevel.Count)
      activeCharacterIndex = 0;
    else
      activeCharacterIndex++;
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
      CombineActiveWithNearestCharacter();

    if (gameManager.inputManager.abilityAction)
    {
      activeCharacter.abilityIndex = 0;
      activeCharacter.ActivateAbility();
    }

    if (gameManager.inputManager.abilityAction2)
    {
      Depression depression = activeCharacter.GetComponent<Depression>();

      if (!depression)
        return;

      if (depression.jetpackActivated)
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

  void CombineActiveWithNearestCharacter()
  {
    List<Character> activeCharactersInLevel = GetActiveCharactersAsList();

    if (activeCharactersInLevel.Count <= 1)
      return;

    Character nearestCharacter = null;
    float smallestCharacterDistance = maxGroupingDistance;

    foreach (Character character in activeCharactersInLevel)
    {
      if (character == activeCharacter)
        continue;

      float distance = Vector2.Distance(activeCharacter.transform.position, character.transform.position);
      
      if (distance > smallestCharacterDistance)
        continue;

      smallestCharacterDistance = distance;
      nearestCharacter = character;
    }

    if (!nearestCharacter)
      return;

    SpawnFusedCharacter(activeCharacter, nearestCharacter);
  }
}
