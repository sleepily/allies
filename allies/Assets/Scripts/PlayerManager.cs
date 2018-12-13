using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
  public GameManager gm;

  [Header("Active/Controllable Characters")]
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
      if (maxGroupingDistance > Vector2.Distance(activeCharacter.transform.position, otherCharacter.transform.position))
      {
        Debug.DrawRay(otherCharacter.transform.position, Vector2.up, Color.cyan);
        allies.Add(otherCharacter);
        allyString += otherCharacter.name + "\n";
      }
    }

    gm.ui.alliesText.text = allyString;
  }

  void SetPlayerManagerParent()
  {
    foreach (Character c in characters)
    {
      c.pm = this;
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
    if (activeCharacter.rb == null)
      return;

    if (gm.im.switchAction)
      SetNextCharacterAsActive();

    if (gm.im.reloadScene)
      SceneManager.LoadScene(0);
  }

  void CalculateMovement()
  {
    var inputX = Input.GetAxisRaw("Horizontal");
    float movement = inputX * movementForce * Time.deltaTime;
    Vector3 horizontalForce = Vector3.right * movement * movementForce;

    float inputY = Input.GetKeyDown(KeyCode.W) ? 1f : 0f;
    inputY = Mathf.Clamp01(inputY); // make sure the value stays at 0 or higher
    Vector3 verticalForce = Vector3.up * jumpForce * inputY;

    foreach (Character ally in allies)
    {
      ally.Move(horizontalForce);
      ally.Jump(verticalForce);
    }
  }
}
