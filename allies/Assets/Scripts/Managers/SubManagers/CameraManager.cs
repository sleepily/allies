using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SubManager
{
  public Camera mainCamera;
  public float transitionSpeed = 1f;

  public List<Camera> views;
  public Camera currentView; // Use this for initialization
  public int viewIndex = 0;

  public override void Init()
  {
    base.Init();

    currentView = views[0];
  }

  private void LateUpdate()
  {
    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, currentView.transform.position, Time.deltaTime * transitionSpeed);

    mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, currentView.orthographicSize, Time.deltaTime * transitionSpeed);

    Vector3 updatedAngle =
      new Vector3
      (
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
      );

    mainCamera.transform.eulerAngles = updatedAngle;
  }

  private void Update()
  {
    GetInput();
    currentView = views[viewIndex];
  }

  void GetInput()
  {
    if (gameManager.inputManager.cameraSwitch)
    {
      viewIndex++;

      if (viewIndex >= views.Count)
        viewIndex = 0;
    }
  }
}