using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Physics")]
  public bool allowJump, allowMove;
  public Rigidbody2D rb;
  public bool isColliding;

  public PlayerManager pm;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();

    switch(name)
    {
      case "Rage":
        allowMove = false;
        allowJump = true; //for testing
        break;
      case "Anxiety":
        allowMove = true;
        allowJump = true;
        break;
      case "Depression":
        allowMove = true;
        allowJump = true;
        break;
      default:
        allowMove = false;
        allowJump = false;
        break;
    }
  }
  
  private void Update()
  {
    // @TODO: fix this later
    if (rb.velocity.y <= 0)
      isColliding = true;

    if (name == "Depression" && isColliding)
      allowJump = true;
    else
      allowJump = false;

    isColliding = false;
  }

  public void Move(Vector3 horizontalForce)
  {
    if (!allowMove)
      return;

    rb.AddForce(horizontalForce);
  }

  public void Jump(Vector3 verticalForce)
  {
    if (!allowJump)
      return;

    rb.AddForce(verticalForce);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    string cName = collision.gameObject.name;

    foreach (Character c in pm.characters)
    {
      if (c.name == cName)
        pm.allies.Add(c);
    }
  }
}