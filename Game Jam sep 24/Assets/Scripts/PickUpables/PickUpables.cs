using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpables : MonoBehaviour
{
    public Dialogue dialogue;

    GameObject player;

    [SerializeField, Tooltip("Set this if Ways == GetClose")]
    float Talkdistance;

    bool talking = false;

    DialogueManager manager;

    float Timer;

    [SerializeField, Tooltip("The item description for this object in Inventory")]
    string Description;

    [SerializeField, Tooltip("The Image for this object in the inventory")]
    Sprite ItemSprite;

    private void Start()
    {
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
        FindObjectOfType<GameManager>().UpdateInventory(Description, ItemSprite);
        Destroy(gameObject);
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
