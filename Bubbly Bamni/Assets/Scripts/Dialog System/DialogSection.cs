using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogSection
{
    [SerializeField] DialogLine[] dialogLines;
    [SerializeField] bool breakAfterPlay;
    [SerializeField] ClickingInteractable targetClickingInteractable;

    public DialogLine[] GetDialogLines => dialogLines;

    public bool IsBreakAfterPlay => breakAfterPlay;
    public ClickingInteractable GetTargetClickingInteractable => targetClickingInteractable;
}
