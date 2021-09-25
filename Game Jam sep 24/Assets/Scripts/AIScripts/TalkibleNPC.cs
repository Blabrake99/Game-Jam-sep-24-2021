﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkibleNPC : MonoBehaviour
{
    public Dialogue dialogue;

    GameObject player;

    [SerializeField, Tooltip("Set this if Ways == GetClose")]
    float Talkdistance;

    bool talking = false;

    DialogueManager manager;

    float Timer;
    private void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {

        if (player == null)
        {
            //this needs to be here because the player can switch characters
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < Talkdistance)
            {
                if (Input.GetButtonDown("Interact") && !talking)
                {
                    TriggerDialogue();
                    Timer = .5f;
                }
                //this is a way for the player to skip dialogue if he's talking to the npc
                if (Input.GetButtonDown("Pause") && talking)
                {
                    EndDialogue();
                }
                //return means enter
                if (talking && Input.GetButtonDown("Enter") && Timer < 0)
                {
                    manager.DisplayNextSentence();
                    Timer = .5f;
                }
            }
            else
            {
                talking = false;
            }
        }
        Timer -= Time.deltaTime;


    }
    public void DoneTalking()
    {
        player.GetComponent<PlayerMovement>().CantMove = false;
        talking = false;
    }
    /// <summary>
    /// Call this when you need to talk to the player 
    /// </summary>
    public void TriggerDialogue()
    {
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
