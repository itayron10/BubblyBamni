using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    [TextArea(2, 5)]
    [SerializeField] string line;
    [SerializeField] SoundScriptableObject audioLine;
    public string GetLineText => line;
    [SerializeField] string nameOfSpeaker;
    public string GetSpeakerName => nameOfSpeaker;
    [SerializeField] float textStayDuration;
    public float GetTextDuration => textStayDuration;
    public SoundScriptableObject GetAudioLine => audioLine;
}
