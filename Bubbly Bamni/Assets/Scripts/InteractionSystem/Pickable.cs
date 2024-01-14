using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    [SerializeField] SoundScriptableObject hittingSound;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (soundManager) soundManager.PlaySound(hittingSound);
    }
}
