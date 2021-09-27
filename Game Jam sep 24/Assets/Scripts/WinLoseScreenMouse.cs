using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseScreenMouse : MonoBehaviour
{
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
