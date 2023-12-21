using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI testText;
    private Tape playedTape;
    public void SetTape(Tape tape) => playedTape = tape;


    private void Update()
    {
        testText.text = playedTape.GetName;
    }
}
