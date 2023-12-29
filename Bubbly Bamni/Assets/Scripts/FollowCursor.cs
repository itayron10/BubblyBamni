using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    private Vector3 zeroPos;

    private void Start()
    {
        zeroPos = transform.position;
    }

    void Update()
    {
        Cursor.visible = false;
        if (Cursor.lockState != CursorLockMode.Locked) transform.position = Input.mousePosition;    
        else transform.position = zeroPos;
    }
}
