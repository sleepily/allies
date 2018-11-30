using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public List<GameObject> playerList = new List<GameObject>();
  GameObject player;
  Rigidbody rb;
  public float speed = 5f;
  public float jumpForce = 10f;
  
	void Start ()
  {
    player = playerList[0];
    rb = player.GetComponent<Rigidbody>();
	}
	
	void Update ()
  {
    GetInput();
	}

  void GetInput()
  {
    var input = Input.GetAxisRaw("Horizontal");
    float movement = input * speed * Time.deltaTime;
    player.transform.position += new Vector3(movement, 0, 0);

    float verticalInput = Input.GetKeyDown(KeyCode.W) ? 1f : 0f;
    verticalInput = Mathf.Clamp01(verticalInput);
    Vector3 force = Vector3.up * jumpForce * verticalInput;
    rb.AddForce(force);
  }
}
