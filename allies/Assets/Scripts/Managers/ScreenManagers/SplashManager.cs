using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SplashManager : SubManager
{
  [Header("Scene Specifics")]

  public VideoPlayer videoPlayer;
  public SpriteFade spriteFade;
  
  bool introStarted = false;

  public bool logoIsSkippable = false;
  public bool videoIsSkippable = false;

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

  private void Update()
  {
    CheckSkipAction();
    CheckVideoStart();
    CheckVideoStop();
  }
  
  void CheckSkipAction()
  {
    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
    {
      if (logoIsSkippable || gameManager.isPlaytest)
        if (!spriteFade.isFinished)
          spriteFade.FinishFade();

      if (videoIsSkippable || gameManager.isPlaytest)
        if (videoPlayer.isPlaying)
          StopVideo();
    }
  }

  void CheckVideoStart()
  {
    if (introStarted)
      return;

    if (!spriteFade.isFinished)
      return;

    PlayVideo();
  }

  void CheckVideoStop()
  {
    if (introStarted && !videoPlayer.isPlaying)
      StopVideo();
  }

  void PlayVideo()
  {
    introStarted = true;
    videoPlayer.Play();
  }

  void StopVideo()
  {
    videoPlayer.Stop();
    gameManager.sceneManager.LoadScreen(SceneManager.Screen.mainMenu);
  }
}
