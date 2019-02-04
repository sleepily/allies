using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AssignMainCamera : MonoBehaviour
{
  public enum Destination
  {
    menuManager,
    canvas,
    videoPlayer
  }

  public Destination destination;

  MenuManager menuManager;
  Canvas canvas;
  VideoPlayer videoPlayer;

  private void Start()
  {
    switch (destination)
    {
      case Destination.menuManager:
        menuManager = GetComponent<MenuManager>();

        if (!menuManager)
          return;

        if (GameManager.globalCamera)
          menuManager.cameraToMove = GameManager.globalCamera;

        break;

      case Destination.canvas:
        canvas = GetComponent<Canvas>();

        if (!canvas)
          return;

        if (GameManager.globalCamera)
          canvas.worldCamera = GameManager.globalCamera;
        
        break;

      case Destination.videoPlayer:
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
          return;

        if (GameManager.globalCamera)
          videoPlayer.targetCamera = GameManager.globalCamera;

        break;
    }
  }
}
