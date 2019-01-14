using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;

  public float fadeInTime = 1f;
  public float holdTime = 1f;
  public float fadeOutTime = 1f;
  public bool finishedFade = false;

	void Start ()
  {
		
	}
	
	void Update ()
  {
    Fade();
	}

  void Fade()
  {
    float alpha = spriteRenderer.color.a;
  }
}
