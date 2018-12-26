using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesignCamera : MonoBehaviour
{
  public Camera dummyCamera;

	void Start ()
  {
    dummyCamera = GetComponent<Camera>();
    dummyCamera.targetDisplay = 5;
	}
}
