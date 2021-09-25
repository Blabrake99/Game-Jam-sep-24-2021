using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkibleNPC : MonoBehaviour
{
    public Dialogue dialogue;


    public Dialogue Question1;
    public Dialogue Question2;
    public Dialogue Question3;

    GameObject player;

    [SerializeField, Tooltip("Set this if Ways == GetClose")]
    float Talkdistance;

    bool talking = false;

    DialogueManager manager;

    float Timer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
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
                if (talking && Input.GetButtonDown("Interact") && Timer < 0)
                {
                    manager.DisplayNextSentence();
                    Timer = .5f;
                }
            }
            else
            {
                talking = false;
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
