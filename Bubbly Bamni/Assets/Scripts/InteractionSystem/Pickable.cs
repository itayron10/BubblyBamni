using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    public string nameId;
    private PlayerInventory playerInventory;


    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public override void Interacte()
    {
        if (playerInventory) playerInventory.SetCurrentItem(this);
        base.Interacte();
    }
}
