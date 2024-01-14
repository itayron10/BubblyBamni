using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    [SerializeField] string nameOfSpeaker;
    [TextArea(2, 5)]
    [SerializeField] string line;
    [SerializeField] SoundScriptableObject audioLine;
    [SerializeField] string animationName;
    [SerializeField] float lineWritingLetterDelay = 0.02f;
    [SerializeField] float textStayDuration;
    public string GetSpeakerName => nameOfSpeaker;
    public string GetAnimationName => animationName;
    public float GetTextDuration => textStayDuration;
    public float GetLineWritingLetterDelay => lineWritingLetterDelay;
    public string GetLineText => line;
    public SoundScriptableObject GetAudioLine => audioLine;
}
