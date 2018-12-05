using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public float transitionSpeed = 1f;
  public List<Camera> views;
  public Camera currentView; // Use this for initialization
  public int viewIndex = 0;
  public Camera mainCamera;

  public SpriteRenderer visibleWithLayers;

  private void Start()
  {
  }

  private void LateUpdate()
  {
    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, currentView.transform.position, Time.deltaTime * transitionSpeed);

    mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, currentView.orthographicSize, Time.deltaTime * transitionSpeed);

    Vector3 currentAngle =
      new Vector3
      (
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
      );

    mainCamera.transform.eulerAngles = currentAngle;
  }

  private void Update()
  {
    visibleWithLayers.enabled = (viewIndex == 1);

    GetInput();
    currentView = views[viewIndex];
  }

  void GetInput()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      viewIndex++;

      if (viewIndex >= views.Count)
        viewIndex = 0;
    }
  }
}