using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] string tapeName;
    [SerializeField] float tapeStartDelay;
    [SerializeField] GameObject worldToActivate;
    public GameObject GetWorldToActivate => worldToActivate;
    public float GetTapeStartDelay => tapeStartDelay;
    public string GetName => tapeName;
    public DialogSection[] episodeDialog;

    private void Start()
    {
        worldToActivate.SetActive(false);
    }
}
