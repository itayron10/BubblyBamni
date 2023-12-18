using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Transform holdingPoint;
    private Pickable currentItem;

    public void SetCurrentItem(Pickable item)
    {
        if (currentItem != null)
        {
            currentItem.transform.SetParent(null);
            if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = false;
            if (currentItem.TryGetComponent<Collider>(out Collider collider)) collider.enabled = true;
        }
        
        currentItem = item;

        currentItem.transform.SetParent(holdingPoint);
        currentItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        if (item.TryGetComponent<Rigidbody>(out Rigidbody newRb)) newRb.isKinematic = true;
        if (item.TryGetComponent<Collider>(out Collider newCollider)) newCollider.enabled = false;
    }
}
