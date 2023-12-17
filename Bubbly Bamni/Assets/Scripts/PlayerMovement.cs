using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float footstepsCooldown;
    [SerializeField] SoundScriptableObject footstepsSound;
    private SoundManager soundManager;
    private float footstepsTimer;
    private Rigidbody rb;
    private InputManager instnace;


    private void Start()
    {
        instnace = InputManager.Instance;
        soundManager = FindObjectOfType<SoundManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 inputDir = instnace.inputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movmentDir = transform.TransformDirection(new Vector3(inputDir.x, 0f, inputDir.y)) * speed; 
        rb.velocity = new Vector3(movmentDir.x, rb.velocity.y, movmentDir.z);
        if (movmentDir.sqrMagnitude > 0f)
        {
            footstepsTimer += Time.deltaTime;
            if (footstepsTimer >= footstepsCooldown)
            {
                soundManager.PlaySound(footstepsSound);
                footstepsTimer = 0f;
            }
        }
        else
        {
            footstepsTimer = 0f;
        }
    }
}
