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
  }
}
