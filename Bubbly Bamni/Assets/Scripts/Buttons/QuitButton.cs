using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : EventButton
{
    public override void OnClick()
    {
        base.OnClick();
        LevelManager.ExitGame();
    }
}
