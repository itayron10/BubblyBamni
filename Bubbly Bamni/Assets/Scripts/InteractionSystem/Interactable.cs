using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    ///NOTE: interactables need a collider so that the active interactable check will work

    [Header("Settings")]
    [Tooltip("This Sound will be played when the player interacts with this interactable")]
    // the sound which will be played when the player interacts with this interactable
    [SerializeField] SoundScriptableObject onInteractionSound;

    private SoundManager soundManager;
    private bool initialized;

    private void Start() => FindPrivateObjects();

    /// <summary>
    /// checks if this interactable can be interacted by the intercter
    /// </summary>
    public bool IsInteractable() => Interacter.instance.GetActiveInteractable == this;

    /// <summary>
    /// can be ovveriden to find different kinds of private objects on the Start method
    /// </summary>
    public virtual void FindPrivateObjects()
    {
        // set/find private objects
        soundManager = FindObjectOfType<SoundManager>();
        initialized = true;
    }

    /// <summary>
    /// can be ovveriden for different behaviours on interaction
    /// </summary>
    public virtual void Interacte()
    {
        if (!initialized) FindPrivateObjects();
        soundManager.PlaySound(onInteractionSound);
        Debug.Log($"You Interacted With this object: {gameObject.name}");
    }
}
