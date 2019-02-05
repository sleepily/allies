using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Parallax script for GameObjects. Scales strength with objects' Z positions.
 */
public class ParallaxParent : MonoBehaviour
{
  [Header("Properties")]
  public float focusDepth = 3.5f;
  public float parallaxStrength = 1f;
  public float maxMouseDistance = 8f;

  [Header("Objects")]
  public List<GameObject> parallaxObjects;

  private void Update()
  {
    Vector2 mousePosition = MouseTools.GetMouseDistanceFromCenter() / 10;

    if (mousePosition.magnitude > maxMouseDistance)
      mousePosition = mousePosition.normalized * maxMouseDistance;

    foreach (GameObject parallaxObject in parallaxObjects)
    {
      Vector3 oldPosition = parallaxObject.transform.position;
      float depthMultiplier = (oldPosition.z - focusDepth);
      Vector2 newPosition2 = mousePosition * (depthMultiplier * parallaxStrength);
      Vector3 newPosition3 = new Vector3(newPosition2.x, newPosition2.y, oldPosition.z);

      string parallaxInfo =
        string.Format
        (
          "Translating {0} from {1} to {2}, multiplied by {3}",
          parallaxObject.name,
          oldPosition,
          newPosition3,
          depthMultiplier
        );

      // Debug.Log(parallaxInfo);

      parallaxObject.transform.position = newPosition3;
    }
  }
}
