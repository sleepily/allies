using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyWall : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D collision)
  {
    CharacterCollisionWithWall(collision, true);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    CharacterRecollisionWithWall(collision);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    CharacterCollisionWithWall(collision, false);
  }

  void CharacterCollisionWithWall(Collision2D collision, bool disableMove)
  {
    if (!collision.gameObject.CompareTag("Character"))
      return;

    Character character = collision.gameObject.GetComponent<Character>();

    character.isCollidingWithWall = disableMove;
  }

  void CharacterRecollisionWithWall(Collision2D collision)
  {
    Character character = collision.gameObject.GetComponent<Character>();

    if (!character)
      return;

    if (character.isCollidingWithGround)
      character.isCollidingWithWall = true;
  }
}
