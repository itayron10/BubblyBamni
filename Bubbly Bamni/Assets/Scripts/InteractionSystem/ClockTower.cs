using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTower : Interactable
{
    [SerializeField] Pickable currentItemForNextTape;
    [SerializeField] Transform dropingPos;
    private PlayerInventory playerInventory;

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public override void Interacte()
    {
        base.Interacte();
        if (playerInventory.GetCurrentItem != null)
        {
            if (playerInventory.GetCurrentItem.TryGetComponent<PuzzleItem>(out PuzzleItem puzzleItem))
            {
                Instantiate(puzzleItem.GetTape.gameObject, dropingPos.position, dropingPos.rotation);
            }
        }
    }
}
