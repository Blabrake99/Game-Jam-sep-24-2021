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

    RaycastHit HitInfo;

    Ray ray;

    [SerializeField, Tooltip("The Q Text in the scene")] GameObject QText;

    [HideInInspector] public bool Talking;
    [HideInInspector] public float timer;

    Pause pause;
    public bool CanMoveCamera;
    private void Start()
    {
        pause = FindObjectOfType<Pause>();

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
        if (!Talking && timer <= 0)
        {
            int layerMask = 1 << 8;
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out HitInfo, 100, layerMask))
            {
                
                //this is here to let you know that there's a clue to pick up
                if (HitInfo.collider.gameObject.tag == "PickUpables")
                    QText.SetActive(true);

                if (HitInfo.collider.gameObject.tag == "Interactable")
                    QText.SetActive(true);



                if (HitInfo.collider.gameObject.tag == "PickUpables" && Input.GetButtonDown("Interact"))
                {
                    HitInfo.collider.gameObject.GetComponent<PickUpables>().TriggerDialogue();
                    Talking = true;
                }

                if (HitInfo.collider.gameObject.tag == "Interactable" && Input.GetButtonDown("Interact"))
                {
                    HitInfo.collider.gameObject.GetComponent<Interactables>().TriggerDialogue();
                    Talking = true;
                }

                if (HitInfo.collider.gameObject.tag != "Interactable" &&
                    HitInfo.collider.gameObject.tag != "PickUpables")
                {
                    QText.SetActive(false);
                }
            }
            else
            {
                QText.SetActive(false);
            }
        }
        else
        {
            //this DeActivates the text when interacting with clue
            QText.SetActive(false);
        }

        if (!pause.isPaused && CanMoveCamera)
        {
            transform.position = Pivot.position + new Vector3(0, yOffset, 0);

            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;

            rotationOnX -= mouseY;
            rotationOnX = Mathf.Clamp(rotationOnX, -90, 90);
            transform.localEulerAngles = new Vector3(rotationOnX, 0, 0);

            target.Rotate(Vector3.up * mouseX);
        }
        if (timer > -.1f)
            timer -= Time.deltaTime;
    }
}
