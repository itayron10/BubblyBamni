using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvInteractable : Interactable
{
    private PlayerInventory playerInventory;
    private ShowManager showManager;

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        showManager = FindObjectOfType<ShowManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public override void Interacte()
    {
        base.Interacte();
        if (playerInventory.GetCurrentItem != null)
        {
            if (playerInventory.GetCurrentItem.TryGetComponent<Tape>(out Tape tape))
            {
                showManager.StartEpisode(tape);
            }
        }
    }

}
