using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickingInteractableManager : MonoBehaviour
{
    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
        inputManager.inputActions.Player.Click.performed += Click_performed;
    }

    private void Click_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log(Camera.main);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.TryGetComponent<ClickingInteractable>(out ClickingInteractable clickingInteractable))
            {
                clickingInteractable.ClickedOn();
            }
        }
    }
}
