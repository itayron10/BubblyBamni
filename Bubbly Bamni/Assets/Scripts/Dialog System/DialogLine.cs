using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    [TextArea(4, 20)]
    [SerializeField] string line;
    [SerializeField] string nameOfSpeaker;
}
