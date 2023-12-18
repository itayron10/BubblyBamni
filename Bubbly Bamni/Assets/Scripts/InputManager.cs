using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public Inputs inputActions;


    private void Awake()
    {
        inputActions = new Inputs();
        inputActions.Enable();

        // Ensure there is only one instance of this class
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    /// <summary>
    /// this method returns the x and y mouse delta position (x delta position, y delta position)
    /// </summary>
    public (float, float) GetMouseDeltaPosXandY()
    {
        Vector2 mouseDelta = inputActions.Camera.MouseDelta.ReadValue<Vector2>();
        return (mouseDelta.x, mouseDelta.y);
    }

}
