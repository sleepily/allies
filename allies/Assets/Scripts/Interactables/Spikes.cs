using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Interactable
{
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Character"))
      gameManager.levelManager.Retry();
  }
}
