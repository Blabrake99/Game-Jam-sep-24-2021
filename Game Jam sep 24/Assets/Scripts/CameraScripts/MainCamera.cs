using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField, Tooltip("How fast the mouse moves the camera")] float MouseSensitivity;

    [SerializeField, Tooltip("The player")] Transform target;

    [SerializeField, Tooltip("The Pivot of the player")] Transform Pivot;

    [SerializeField, Tooltip("How high the camera would be above the ground")] float yOffset;

    private float rotationOnX;

    private void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerMovement>().gameObject.transform;
            Pivot = target.GetChild(0).transform;
        }

        if (transform.parent == null)
            transform.SetParent(target.GetChild(0).transform);
    }

    void Update()
    {
        transform.position = Pivot.position +  new Vector3(0, yOffset, 0);

        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;

        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -90, 90);
        transform.localEulerAngles = new Vector3(rotationOnX, 0, 0);

        target.Rotate(Vector3.up * mouseX);
    }
}
