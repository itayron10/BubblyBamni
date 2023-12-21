using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] string tapeName;
    public string GetName => tapeName;
    [SerializeField] DialogSection[] dialogSections;
}
