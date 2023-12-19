using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBoolSetting : MonoBehaviour
{
    [SerializeField] string valueNameToUpdate;
    [SerializeField] SettingsValuesSO settingsValues;
    [SerializeField] Toggle toggle;

    private void Start()
    {
        UpdateSettings(false);
    }

    public void UpdateSettings(bool fromSlider)
    {
        for (int i = 0; i < settingsValues.boolSettings.Length; i++)
        {
            if (settingsValues.boolSettings[i].valueName == valueNameToUpdate)
            {
                bool settingValue = fromSlider ? toggle.isOn : settingsValues.boolSettings[i].value;
                if (fromSlider)
                    settingsValues.boolSettings[i].value = toggle.isOn;
                else
                    toggle.isOn = settingsValues.boolSettings[i].value;

                settingsValues.boolSettings[i].onSettingsChanges?.Invoke(settingValue);
            }
        }
    }
}
