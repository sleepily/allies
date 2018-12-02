using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("Physics")]
  public bool allowJump, allowMove;
  public Rigidbody2D rb;
  public bool isColliding;

  public enum State
  {
    idle,
    move,
    jump,
    freeze,
    ability
  }

  public State state;

  public PlayerManager pm;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  
  private void Update()
  {

  }

  private void FixedUpdate()
  {
    GetGlobalGravityScale();
  }

  void GetGlobalGravityScale()
  {
    rb.gravityScale = pm.globalGravityScale;
  }

  public void Move(Vector2 horizontalForce)
  {
    if (!allowMove)
      return;

    state = State.move;

    rb.velocity += horizontalForce * rb.gravityScale;
  }

  public void Jump(Vector2 verticalForce)
  {
    if (!allowJump || !isColliding)
      return;

    state = State.jump;

    rb.velocity += verticalForce * rb.gravityScale;
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    // @TODO: clean up and fix this
    /*
    string cName = collision.gameObject.name;

    foreach (Character c in pm.characters)
    {
      if (c.name == cName)
        pm.allies.Add(c);
    }
    */
    
    isColliding = false;

    foreach (ContactPoint2D cp in collision.contacts)
      if (cp.point.y < transform.position.y + (Vector2.down * .3f).y)
      {
        isColliding = true;
        Debug.DrawRay(cp.point, cp.normal, Color.green);
      }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    isColliding = false;
  }
}