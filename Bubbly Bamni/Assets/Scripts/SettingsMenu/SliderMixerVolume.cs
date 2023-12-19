using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SliderMixerVolume : MonoBehaviour
{
    private Slider slider;
    [SerializeField] AudioMixer masterMix;
    [SerializeField] string mixerName;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        SetMixLevel(0f);
        slider.onValueChanged.AddListener(SetMixLevel);
    }

    public void SetMixLevel(float sliderVal)
    {
        masterMix.SetFloat(mixerName, sliderVal);
        masterMix.GetFloat(mixerName, out float val);
    }
}
