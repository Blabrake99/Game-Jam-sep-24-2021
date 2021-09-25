using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField, Tooltip("The pause screen")] GameObject pauseObject;


    GameManager Gm;

    GameObject player;

    float pauseTimer;

    public bool isPaused;

    void Start()
    {
        Gm = GetComponent<GameManager>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (pauseObject == null)
        {
            pauseObject = GameObject.FindGameObjectWithTag("Pause_Menu");
            pauseObject.SetActive(false);
        }
        else
        {
            pauseObject.SetActive(false);
        }
    }
    void Update()
    {
        if (pauseObject != null)
        {
            if (Input.GetButtonDown("Pause") && pauseTimer <= 0)
            {
                isPaused = !isPaused;
                pause(isPaused);
                pauseTimer = .2f;
            }
            if (player == null)
            {
                player = FindObjectOfType<PlayerMovement>().gameObject;
            }
        }
        pauseTimer -= Time.unscaledDeltaTime;
    }
    public void pause(bool pause)
    {
        if (pause)
        {
            player.GetComponent<PlayerMovement>().CantMove = true;
            Gm.unlockCursor();
            pauseObject.SetActive(true);
        }
        else
        {
            player.GetComponent<PlayerMovement>().CantMove = false;
            Gm.lockCursor();
            pauseObject.SetActive(false);
        }
    }
}
