using UnityEngine;

namespace Tools
{
  public static class ExtensionMethods
  {
    public static float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
      return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static float Map01(float value, float fromSource, float toSource)
    {
      return (value - fromSource) / (toSource - fromSource);
    }

    public static float VectorToAngle(Vector2 toAngle)
    {
      return Mathf.Atan2(toAngle.y, toAngle.x) * Mathf.Rad2Deg;
    }

    public static Vector2 AngleToVector(float angle, bool isRadians = false)
    {
      if (isRadians)
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
      else
        return AngleToVector(angle * Mathf.Deg2Rad, true);
    }
  }
}
