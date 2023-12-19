using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateFloatSetting : MonoBehaviour
{
    [SerializeField] string valueNameToUpdate;
    [SerializeField] SettingsValuesSO settingsValues;
    [SerializeField] Slider slider;

    private void Awake() => UpdateSettings(false);

    public void UpdateSettings(bool fromSlider)
    {
        for (int i = 0; i < settingsValues.floatSettings.Length; i++)
        {
            if (settingsValues.floatSettings[i].valueName == valueNameToUpdate)
            {
                float settingValue = fromSlider ? slider.value : settingsValues.floatSettings[i].value;
                if (fromSlider)
                    settingsValues.floatSettings[i].value = slider.value;
                else
                    slider.value = settingsValues.floatSettings[i].value;

                settingsValues.floatSettings[i].onSettingsChanges?.Invoke(settingValue);
            }
        }
    }
}
