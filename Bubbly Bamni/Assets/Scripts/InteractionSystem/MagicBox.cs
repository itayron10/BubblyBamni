using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBox : Interactable
{
    [SerializeField] int currentItemNeededIndex;
    [SerializeField] ItemNeeded[] correctItems;
    [SerializeField] Transform itemPedistolParant;
    [SerializeField] string isOpenAnimatorBool;
    [SerializeField] float processingTime;
    [SerializeField] float wrongItemThrowPower;
    private Pickable itemInBox;
    private PlayerInventory playerInventory;
    private Animator animator;

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        playerInventory = FindObjectOfType<PlayerInventory>();
        animator = GetComponent<Animator>();
        foreach (var correctItem in correctItems)
        {
            if (correctItem.tapeGivenInstance.TryGetComponent<Rigidbody>(out Rigidbody tapeRb)) tapeRb.isKinematic = true;
            correctItem.tapeGivenInstance.gameObject.SetActive(false);
        }
        
    }

    public override void Interacte()
    {
        base.Interacte();
        if (itemInBox != null) { return; }
        StartCoroutine(ProcessItem());
    }

    private IEnumerator ProcessItem()
    {
        itemInBox = playerInventory.GetCurrentItem;
        playerInventory.SetCurrentItem(null);
        itemInBox.transform.SetParent(itemPedistolParant);
        if (itemInBox.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = true;
        if (correctItems[currentItemNeededIndex].tapeGivenInstance.TryGetComponent<Rigidbody>(out Rigidbody tapeRb)) tapeRb.isKinematic = true;
        itemInBox.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        
        
        animator.SetBool(isOpenAnimatorBool, false);
        yield return new WaitForSeconds(processingTime);

        if (itemInBox.nameId == correctItems[currentItemNeededIndex].item.nameId)
        {
            correctItems[currentItemNeededIndex].tapeGivenInstance.gameObject.SetActive(true);
            Destroy(itemInBox.gameObject);
            currentItemNeededIndex++;
        }
        else
        {
            itemInBox = null;
        }

        yield return new WaitForSeconds(processingTime);
        animator.SetBool(isOpenAnimatorBool, true);
    }
}

[System.Serializable]
public struct ItemNeeded
{
    public Pickable item;
    public Tape tapeGivenInstance;
}
