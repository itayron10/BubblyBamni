using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interacter : MonoBehaviour
{
    // NOTE: this script should be on the player and it is kind of the "interaction manager"
    // but also it is very important that it will be on the player transform for the active interactable to work

    [Header("Refernecs")]
    // the layer mask of the interactable objects
    [SerializeField] LayerMask interactionLayerMask;
    private Inputs playerInput;
    // reference for the closest interactable, this interactable will be interacted when we interact
    private Interactable activeInteractable;
    public Interactable GetActiveInteractable => activeInteractable;
    public static Interacter instance;

    [Header("Settings")]
    // how far away we can interact with interactables
    [SerializeField] float interactionRadius;


    private void Awake()
    {
        SetSingelton();
    }

    private void Start()
    {
        SubscribeToInput();
    }

    private void FixedUpdate() => CheckForInteractables();

    private void SetSingelton() => instance = this;

    private void SubscribeToInput()
    {
        // set the player input based on the input manager
        playerInput = InputManager.Instance.inputActions;
        // subscribes to the interact action
        playerInput.Player.Interact.performed += Interact_performed;
    }
    
    /// <summary>
    /// this method checks the interactabels in the interaction radius and set the closest one to be the active interactable
    /// </summary>
    private void CheckForInteractables()
    {
        // get the collider infornt of you
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRadius, interactionLayerMask);
        // cheak if the collider is interactable
        if (hit.collider == null) { activeInteractable = null; return; }
        if (!hit.collider.TryGetComponent<Interactable>(out Interactable interactable)) { activeInteractable = null; return; }
        // set as the active interactable
        activeInteractable = interactable;
    }

    /// <summary>
    /// this method takes the list of colliders in the interaction radius and converts it to a list of interactables
    /// </summary>
    private List<Interactable> HandleInteractablesColliders(Collider[] colliders)
    {
        List<Interactable> interactables = new List<Interactable>();
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
                interactables.Add(interactable);
        }
        return interactables;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        activeInteractable?.Interacte();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
