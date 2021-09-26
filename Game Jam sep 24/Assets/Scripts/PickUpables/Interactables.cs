using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public Dialogue dialogue;

    GameObject player;

    bool talking = false;

    DialogueManager manager;

    float Timer;

    GameManager Gm;
    private void Start()
    {
        Gm = FindObjectOfType<GameManager>();
        manager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        //this is a way for the player to skip dialogue if he's talking to the npc
        if (Input.GetButtonDown("Pause") && talking)
        {
            EndDialogue();
        }
        //return means enter
        if (talking && Input.GetButtonDown("Interact") && Timer < 0)
        {
            manager.DisplayNextSentence();
            Timer = .5f;
        }

        Timer -= Time.deltaTime;


    }
    public void DoneTalking()
    {
        player.GetComponent<PlayerMovement>().CantMove = false;
        FindObjectOfType<MainCamera>().timer = 1f;
        FindObjectOfType<MainCamera>().Talking = false;
    }
    /// <summary>
    /// Call this when you need to talk to the player 
    /// </summary>
    public void TriggerDialogue()
    {
        Timer = .5f;
        talking = true;
        manager.StartDialogue(dialogue);
        manager.NPC = this.gameObject;
        player.GetComponent<PlayerMovement>().CantMove = true;
    }
    public void EndDialogue()
    {
        talking = false;
        manager.EndDialogue();
        player.GetComponent<PlayerMovement>().CantMove = false;
    }
}
