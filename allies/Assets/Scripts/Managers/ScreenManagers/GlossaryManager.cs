﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossaryManager : SubManager
{
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
      gameManager.sceneManager.LoadScreen(SceneManager.Screen.mainMenu);
  }
}
