using UnityEngine;
using UnityEngine.UI;

public class UIManager : SubManager
{
  public Canvas canvas;
  public Text alliesText;

  public override void Init()
  {
    base.Init();

    if (gameManager.cameraManager.mainCamera != null)
      canvas.worldCamera = gameManager.cameraManager.mainCamera;
  }
}