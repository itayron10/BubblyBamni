using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings Menu/Settings")]
public class SettingsValuesSO : ScriptableObject
{
    public SettingValue<float>[] floatSettings;
    public SettingValue<bool>[] boolSettings;
}

[System.Serializable]
public struct SettingValue<T>
{
    public string valueName;
    public delegate void OnSettingsChanges(T setting);
    public OnSettingsChanges onSettingsChanges;
    public T value;
}