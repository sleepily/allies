using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SplashManager : SubManager
{
  [Header("Scene Specifics")]

  public VideoPlayer videoPlayer;
  public SpriteFade spriteFade;

  public float timeBetweenLogoAndVideo = 1f;

  public override void Init()
  {
    base.Init();
    
    PrepareVideoPlayer();
  }

  void PrepareVideoPlayer()
  {
    if (!videoPlayer.isPrepared)
      videoPlayer.Prepare();
  }
}
