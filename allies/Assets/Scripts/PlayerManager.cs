using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  [Header("Active/Controllable Characters")]
  public List<Character> characters = new List<Character>();
  public int characterIndex = 0;
  public Character activeCharacter;
  public List<Character> allies = new List<Character>();

  [Header("Rigidbodies")]
  List<Rigidbody2D> rbs = new List<Rigidbody2D>();
  Rigidbody2D activeRigidbody;

  [Header("Global Character Physics")]
  public float speed = 2f;
  public float jumpForce = 2f;
  public float globalGravityScale = 5f;
  
	void Start ()
  {
    GetCharacterRigidbodies();
    SetPlayerManagerParent();
    SetActiveCharacter();
  }

  void Update ()
  {
    GetInput();
	}

  void SetPlayerManagerParent()
  {
    foreach (Character c in characters)
    {
      c.pm = this;
    }
  }

  void GetCharacterRigidbodies()
  {
    foreach (Character c in characters)
    {
      rbs.Add(c.GetComponent<Rigidbody2D>());
    }
  }

  void SetActiveCharacter()
  {
    activeCharacter = characters[characterIndex];
    activeRigidbody = rbs[characterIndex];
  }

  void NextCharacter()
  {
    if (characterIndex + 1 >= characters.Count)
      characterIndex = 0;
    else
      characterIndex++;

    SetActiveCharacter();
  }

  void GetInput()
  {
    if (activeRigidbody == null)
      return;

    if (Input.GetKeyDown(KeyCode.R))
      NextCharacter();

    var input = Input.GetAxisRaw("Horizontal");
    float movement = input * speed * Time.deltaTime;
    Vector3 horizontalForce = new Vector3(movement * speed, 0, 0);
    activeCharacter.Move(horizontalForce);

    float verticalInput = Input.GetKeyDown(KeyCode.W) ? 1f : 0f;
    verticalInput = Mathf.Clamp01(verticalInput);
    Vector3 verticalForce = Vector3.up * jumpForce * verticalInput;
    activeCharacter.Jump(verticalForce);
  }
}
