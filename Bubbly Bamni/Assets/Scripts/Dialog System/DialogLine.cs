using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    [TextArea(2, 5)]
    [SerializeField] string line;
    [SerializeField] string nameOfSpeaker;
}
