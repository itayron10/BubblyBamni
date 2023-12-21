using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogSection
{
    [SerializeField] DialogLine[] dialogLines;
    [SerializeField] bool breakAfterPlay;
}
