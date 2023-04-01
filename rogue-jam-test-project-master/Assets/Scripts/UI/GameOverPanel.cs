using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : PanelBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void QuitGame()
    {
        Debug.Log(1);
        Application.Quit();
    }
}
