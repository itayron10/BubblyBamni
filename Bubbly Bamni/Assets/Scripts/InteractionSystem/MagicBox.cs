using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBox : Interactable
{
    [SerializeField] int currentItemNeededIndex;
    public int GetCurrentItemNeededIndex => currentItemNeededIndex;
    [SerializeField] ItemNeeded[] correctItems;
    [SerializeField] Transform itemPedistolParant;
    [SerializeField] string isOpenAnimatorBool;
    [SerializeField] float processingTime;
    [SerializeField] float wrongItemThrowPower;
    [SerializeField] SoundScriptableObject recieveItem, correctItem, wrongItem;
    private Pickable itemInBox;
    private PlayerInventory playerInventory;
    private Animator animator;
    [SerializeField] AudioSource audioSource;


    private void Update()
    {
        if (itemInBox == null)
        {
            if (currentItemNeededIndex < correctItems.Length)
            {
                if (!audioSource.isPlaying) soundManager.PlaySound(correctItems[currentItemNeededIndex].hintAudio, audioSource);
            }
        }
    }

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
        audioSource.Stop();
        itemInBox = playerInventory.GetCurrentItem;
        playerInventory.SetCurrentItem(null);
        itemInBox.transform.SetParent(itemPedistolParant);
        if (itemInBox.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = true;
        if (correctItems[currentItemNeededIndex].tapeGivenInstance != null) if (correctItems[currentItemNeededIndex].tapeGivenInstance.TryGetComponent<Rigidbody>(out Rigidbody tapeRb)) tapeRb.isKinematic = true;
        itemInBox.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        soundManager.PlaySound(recieveItem);
        
        animator.SetBool(isOpenAnimatorBool, false);
        yield return new WaitForSeconds(processingTime);

        if (correctItems[currentItemNeededIndex].tapeGivenInstance == null || itemInBox.nameId != correctItems[currentItemNeededIndex].item.nameId)
        {
            soundManager.PlaySound(wrongItem);
            itemInBox = null;
        }
        else
        {
            correctItems[currentItemNeededIndex].tapeGivenInstance.gameObject.SetActive(true);
            Destroy(itemInBox.gameObject);
            currentItemNeededIndex++;
            soundManager.PlaySound(correctItem);
        }

        yield return new WaitForSeconds(processingTime);
        animator.SetBool(isOpenAnimatorBool, true);
    }
}

[System.Serializable]
public struct ItemNeeded
{
    public Pickable item;
    public SoundScriptableObject hintAudio;
    public Tape tapeGivenInstance;
}
