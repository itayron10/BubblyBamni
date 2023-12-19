using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrlButton : EventButton
{
    [SerializeField] string url;

    public override void OnClick()
    {
        base.OnClick();
        Application.OpenURL(url);
    }
}
