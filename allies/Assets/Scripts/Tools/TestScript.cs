using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
  public GameManager gameManager;

  public Vector2 mouse;
  public Vector2 character;
  public Vector2 toMouse;
  public float angleToMouse;
  
	void Start ()
  {
		
	}
	
	void Update ()
  {
    if (Input.GetKeyDown(KeyCode.Q))
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
