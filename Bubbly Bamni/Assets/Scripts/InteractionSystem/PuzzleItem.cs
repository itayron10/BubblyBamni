using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : Pickable
{
    [SerializeField] Tape tapeGivenByPuzzle;
    public Tape GetTape => tapeGivenByPuzzle;
}
