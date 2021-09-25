using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHeadFallow : MonoBehaviour
{
    GameObject player;
    public GameObject head;
    Quaternion headStartingRot;
    bool lookAt;
    [SerializeField] float rotateSpeed;
    [SerializeField] float HeadYOffset;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        headStartingRot = head.transform.rotation;
    }
    void Update()
    {
        //looks at player
        if (lookAt)
        {
            head.transform.rotation = lookAtSlowly(head.transform, player.transform.position + new Vector3(0, HeadYOffset, 0), rotateSpeed);
        }
        else
        {
            //makes his head go back to starting pos 
            head.transform.rotation = lookAtSlowly(head.transform, headStartingRot, rotateSpeed);
        }
    }
    Quaternion lookAtSlowly(Transform t, Vector3 target, float speed)
    {
        Vector3 relativePos = target - t.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        return Quaternion.Lerp(t.rotation, toRotation, speed * Time.deltaTime);
    }
    Quaternion lookAtSlowly(Transform t, Quaternion targetRot, float speed)
    {
        return Quaternion.Lerp(t.rotation, targetRot, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            lookAt = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            lookAt = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            lookAt = false;
        }
    }
}
