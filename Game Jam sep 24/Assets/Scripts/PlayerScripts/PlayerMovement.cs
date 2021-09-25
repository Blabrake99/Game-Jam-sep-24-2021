using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float HSpeed;
    [SerializeField] float VSpeed;

    private float Vertical;

    private float Horizontal;

    private Rigidbody rb;

    [Tooltip("This is used for when your talking to someone or when the games paused")] 
    public bool CantMove;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!CantMove)
        {
            Vertical = Input.GetAxis("Vertical") * VSpeed * Time.deltaTime;
            Horizontal = Input.GetAxis("Horizontal") * HSpeed * Time.deltaTime;

            if (Horizontal != 0 || Vertical != 0)
                rb.MovePosition(transform.position + (transform.forward * Vertical * VSpeed) + (transform.right * Horizontal * HSpeed));
        }
    }
}
