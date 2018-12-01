using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public List<GameObject> characters = new List<GameObject>();
  public int characterIndex = 0;
  GameObject activeCharacter;
  List<Rigidbody2D> rbs = new List<Rigidbody2D>();
  Rigidbody2D activeRigidbody;
  public float speed = 5f;
  public float jumpForce = 10f;
  
	void Start ()
  {
    foreach (GameObject c in characters)
      rbs.Add(c.GetComponent<Rigidbody2D>());

    SetActiveCharacter();
  }

  void Update ()
  {
    GetInput();
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
    activeRigidbody.AddForce(horizontalForce);

    float verticalInput = Input.GetKeyDown(KeyCode.W) ? 1f : 0f;
    verticalInput = Mathf.Clamp01(verticalInput);
    Vector3 verticalForce = Vector3.up * jumpForce * verticalInput;
    activeRigidbody.AddForce(verticalForce);
  }
}
