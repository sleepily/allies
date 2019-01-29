using UnityEngine;

public class FloatableObject : Interactable
{
  [Header("Settings")]
  public float floatTime = 1f;

  public float floatHeight = 4f;
  protected Vector2 position_initial;
  protected Vector2 position_final;
  private float timestamp = -1f;

  public override void Init()
  {
    base.Init();

    position_initial = transform.position;
    position_final = position_initial + Vector2.up * floatHeight;
  }

  private void Update()
  {
    Float();
  }

  public override void Activate()
  {
    if (actionActivated)
      return;

    base.Activate();

    UpdateTimestamp();
  }

  public override void Deactivate()
  {
    if (!actionActivated)
      return;

    base.Deactivate();
    UpdateTimestamp();
  }

  private void UpdateTimestamp()
  {
    timestamp = Time.time;
  }

  protected virtual void Float()
  {
    FloatUp();
    FloatDown();
  }

  protected virtual void FloatUp()
  {
    if (!actionActivated)
      return;

    LerpToPosition(position_initial, position_final);
  }

  protected virtual void FloatDown()
  {
    if (actionActivated)
      return;

    LerpToPosition(position_final, position_initial);
  }

  protected void LerpToPosition(Vector2 start, Vector2 end)
  {
    float lerpValue = Tools.ExtensionMethods.Map01(Time.time, timestamp, timestamp + floatTime);
    transform.position = Vector2.Lerp(start, end, lerpValue);
  }
}