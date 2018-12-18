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
  }

  public void ActivateAbility(List<Character> allies)
  {
    Character one = allies[0];

    if (allies.Count > 1)
    {
      Character two = allies[1];
    }

    rampage = true;
  }

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
    gameManager.playerManager.rage.rb.AddForce(Vector2.right * 30);
  }
}