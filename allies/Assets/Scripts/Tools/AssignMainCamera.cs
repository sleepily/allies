using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AssignMainCamera : MonoBehaviour
{
  Canvas canvas;
  VideoPlayer videoPlayer;

  private void Start()
  {
    canvas = GetComponent<Canvas>();

    if (canvas)
    {
      canvas.planeDistance = 10;
      canvas.worldCamera = GameManager.globalCamera;
    }

    videoPlayer = GetComponent<VideoPlayer>();

    if (videoPlayer)
      videoPlayer.targetCamera = GameManager.globalCamera;
  }
}
