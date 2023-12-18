using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Transform holdingPoint;
    private Pickable currentItem;
    private InputManager inputManager;


    private void Start()
    {
        inputManager = InputManager.Instance;
        inputManager.inputActions.Player.Drop.performed += Drop_performed;
    }

    private void Drop_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SetCurrentItem(null);
    }

    public void SetCurrentItem(Pickable item)
    {
        if (currentItem != null)
        {
            currentItem.transform.SetParent(null);
            if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = false;
            if (currentItem.TryGetComponent<Collider>(out Collider collider)) collider.enabled = true;
        }
        
        currentItem = item;

        if (currentItem == null) return;
        currentItem.transform.SetParent(holdingPoint);
        currentItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        if (item.TryGetComponent<Rigidbody>(out Rigidbody newRb)) newRb.isKinematic = true;
        if (item.TryGetComponent<Collider>(out Collider newCollider)) newCollider.enabled = false;
    }
}
