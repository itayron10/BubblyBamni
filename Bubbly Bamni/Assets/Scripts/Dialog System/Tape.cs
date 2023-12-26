using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] string tapeName;
    [SerializeField] float tapeStartDelay;
    public float GetTapeStartDelay => tapeStartDelay;
    public string GetName => tapeName;
    public DialogSection[] episodeDialog;
}
