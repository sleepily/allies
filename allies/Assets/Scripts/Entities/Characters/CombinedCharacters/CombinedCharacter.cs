using UnityEngine;

public class CombinedCharacter : Character
{
  public Character[] combination = new Character[2];

  public void Fusion(Character a, Character b)
  {
    combination[0] = a;
    combination[1] = b;

    SetCombinationActive(false);

    MoveToInitiatorPosition(a.transform.position);

    this.gameObject.SetActive(true);

    gameManager.characterManager.SetActiveCharacter(this);
  }

  protected override void Init()
  {
    GetAllComponents();
    // DeactivateAbility(); DO NOT disable on init. will make character inactive
    isMovingLeft = false;
  }

  void MoveToInitiatorPosition(Vector2 position)
  {
    this.transform.position = position;
  }

  protected override void SetAnimatorVariables()
  {
    animator.SetBool("abilityActive", abilityActive);
  }

  void SetCombinationActive(bool value)
  {
    foreach (Character character in combination)
    {
      character.DeactivateAbility();
      character.gameObject.SetActive(value);
    }
  }

  public override void DeactivateAbility()
  {
    base.DeactivateAbility();

    SetCombinationActive(true);

    characterManager.activeCombinedCharacter = null;
    this.gameObject.SetActive(false);
  }
}