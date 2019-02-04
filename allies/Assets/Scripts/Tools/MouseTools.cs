using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTools : MonoBehaviour
{
  public static Vector2 GetMouseDistanceFromCenter()
  {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // Debug.Log(mousePosition);

    return mousePosition;
  }

  public float GetMouseAngleFromCenter()
  {
    Vector2 mouseFromCenter = GetMouseDistanceFromCenter();
    float angleToMouse = Mathf.Atan2(mouseFromCenter.y, mouseFromCenter.x) * Mathf.Rad2Deg;

    if (angleToMouse < 0)
      angleToMouse += 360;

    // Debug.Log(angleToMouse);

    return angleToMouse;
  }
}
