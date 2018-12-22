using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
  public GameManager gameManager;

  [Header("Abilities")]
  public bool rampage = false; //rag
  public bool crybaby = false; //dep
  public bool coldFeet = false; //anx
  public bool eruption = false; //rag + dep
  public bool frozenOutrage = false; //rag + anx
  public bool iceyTears = false; //dep + anx

  private void Update()
  {
    ExecuteActiveAbilities();
  }

  private void ExecuteActiveAbilities()
  {
    Rampage();
    ColdFeet();
    Crybaby();
  }

  public void ActivateAbility(List<Character> allies)
  {
    Character one = allies[0];

    if (allies.Count > 1)
    {
      Character two = allies[1];
    }

    switch (one.name)
    {
      case "Rage":
        rampage = true;
        break;
      case "Anxiety":
        coldFeet = true;
        break;
      case "Depression":
        crybaby = true;
        break;
      default:
        break;
    }

  }

  //TODO: clean this up/generalize in character class

  void Rampage()
  {
    if (!rampage)
    {
      gameManager.playerManager.rage.allowJump = true;
      gameManager.playerManager.rage.allowMove = true;
      return;
    }

    gameManager.playerManager.rage.allowJump = false;
    gameManager.playerManager.rage.allowMove = false;
    gameManager.playerManager.rage.state = Character.State.ability;
  }

  void ColdFeet()
  {
    if (!coldFeet)
    {
      gameManager.playerManager.anxiety.allowJump = true;
      gameManager.playerManager.anxiety.allowMove = true;
      return;
    }

    gameManager.playerManager.anxiety.allowJump = false;
    gameManager.playerManager.anxiety.allowMove = false;
    gameManager.playerManager.anxiety.state = Character.State.ability;
  }


  void Crybaby()
  {
    if (!crybaby)
    {
      gameManager.playerManager.depression.allowJump = true;
      gameManager.playerManager.depression.allowMove = true;
      return;
    }

    gameManager.playerManager.depression.allowJump = false;
    gameManager.playerManager.depression.allowMove = false;
    gameManager.playerManager.depression.state = Character.State.ability;
  }
}