using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpables : MonoBehaviour
{
    public Dialogue dialogue;

    GameObject player;

    bool talking = false;

    DialogueManager manager;

    float Timer;

    [SerializeField, Tooltip("The item description for this object in Inventory")]
    string Description;

    [SerializeField, Tooltip("The Image for this object in the inventory")]
    Sprite ItemSprite;

    GameManager Gm;
    [SerializeField] ItemsToGive itemGiven;
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
        Gm.UpdateInventory(Description, ItemSprite);
        GivePlayerItem();
        Destroy(gameObject);
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
    #region Don't look at code
    void GivePlayerItem()
    {
        if(itemGiven == ItemsToGive.Watch)
        {
            Gm.hasWatch = true;
        }
        if(itemGiven == ItemsToGive.NameTag)
        {
            Gm.hasNametag = true;
        }
        if(itemGiven == ItemsToGive.ShoePrint)
        {
            Gm.hasShoePrint = true;
        }
        if(itemGiven == ItemsToGive.Hat)
        {
            Gm.hasHat = true;
        }
        if(itemGiven == ItemsToGive.FlashLight)
        {
            Gm.hasFlashLight = true;
        }
        if(itemGiven == ItemsToGive.LunchBox)
        {
            Gm.hasLunchBox = true;
        }
        if(itemGiven == ItemsToGive.Moustache)
        {
            Gm.hasMoustache = true;
        }
        if(itemGiven == ItemsToGive.GlassCut)
        {
            Gm.hasGlassCut = true;
        }
    }
    enum ItemsToGive
    {
        Watch,
        NameTag,
        ShoePrint,
        Hat,
        FlashLight,
        LunchBox,
        Moustache,
        GlassCut
    }
    #endregion
}
