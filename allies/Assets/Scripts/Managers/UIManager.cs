using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public GameManager gameManager;

  public Canvas canvas;
  public Text alliesText;

  private void Start()
  {
    if (gameManager.cameraManager.mainCamera != null)
      canvas.worldCamera = gameManager.cameraManager.mainCamera;
  }
}