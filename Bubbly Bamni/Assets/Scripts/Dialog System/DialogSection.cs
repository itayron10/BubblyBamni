using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogSection
{
    [SerializeField] DialogLine[] dialogLines;
    public DialogLine[] GetDialogLines => dialogLines;

    [SerializeField] bool breakAfterPlay;
}
