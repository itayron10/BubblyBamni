using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvInteractable : Interactable
{
    [SerializeField] SoundScriptableObject startTapeSound, endTapeSound;
    private PlayerInventory playerInventory;
    private ShowManager showManager;

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        showManager = FindObjectOfType<ShowManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        showManager.onEndEpisode += EndEpisode;
    }

    public override void Interacte()
    {
        base.Interacte();
        if (playerInventory.GetCurrentItem != null)
        {
            if (playerInventory.GetCurrentItem.TryGetComponent<Tape>(out Tape tape))
            {
                showManager.StartEpisode(tape);
                soundManager.PlaySound(startTapeSound);
            }
        }
    }

    private void EndEpisode()
    {
        soundManager.PlaySound(endTapeSound);
    }

}
