using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = Input.mousePosition;    
    }
}
