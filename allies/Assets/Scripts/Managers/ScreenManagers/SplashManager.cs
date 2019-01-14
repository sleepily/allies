using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SplashManager : SubManager
{
  [Header("Scene Specifics")]
  public VideoPlayer videoPlayer;

  public override void Init()
  {
    base.Init();

    if (!videoPlayer.isPrepared)
      videoPlayer.Prepare();
  }

  private void Update()
  {

  }
}
