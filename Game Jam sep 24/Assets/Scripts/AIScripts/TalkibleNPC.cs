using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkibleNPC : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue WatchDialogue;
    public Dialogue NameTagDialogue;
    public Dialogue ShoePrintDialogue;
    public Dialogue HatDialogue;
    public Dialogue FlashLightDialogue;
    public Dialogue LunchBoxDialogue;
    public Dialogue MoustacheDialogue;
    public Dialogue GlassCutDialogue;

    GameObject player;

    [SerializeField, Tooltip("Set this if Ways == GetClose")]
    float Talkdistance;

    bool talking = false;

    DialogueManager manager;

    float Timer;

    [SerializeField] GameObject ButtonPage;

    [SerializeField] GameObject StartingButton, button1, button2, button3, button4, button5, button6, button7, button8;
    [SerializeField] string StartingButtonName, button1Name, button2Name, button3Name, button4Name, button5Name, button6Name, button7Name, button8Name;

    GameManager Gm;

    [HideInInspector] public bool Question1Unlocked, 
        Question2Unlocked, Question3Unlocked;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = FindObjectOfType<DialogueManager>();
        Gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < Talkdistance)
        {
            if (Input.GetButtonDown("Interact") && !talking)
            {
                Gm.unlockCursor();
                setButtons();
                ButtonPage.SetActive(true);
                Timer = .5f;
            }
            //this is a way for the player to skip dialogue if he's talking to the npc
            if (Input.GetButtonDown("Pause") && talking)
            {
                Gm.lockCursor();
                ButtonPage.SetActive(false);
                EndDialogue();
            }
            if (Input.GetButtonDown("Pause"))
            {
                Gm.lockCursor();
                ButtonPage.SetActive(false);
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
        Gm.lockCursor();
        ButtonPage.SetActive(false);
        talking = false;
    }
    /// <summary>
    /// Call this when you need to talk to the player 
    /// </summary>
    public void TriggerDialogue(Dialogue log)
    {
        ButtonPage.SetActive(false);
        talking = true;
        manager.StartDialogue(log);
        manager.NPC = this.gameObject;
        player.GetComponent<PlayerMovement>().CantMove = true;
    }
    public void EndDialogue()
    {
        talking = false;
        manager.EndDialogue();
        player.GetComponent<PlayerMovement>().CantMove = false;
    }

    void setButtons()
    {
        //this ready's up all the buttons to start dialogue 
        StartingButton.transform.GetChild(0).GetComponent<Text>().text = StartingButtonName;
        StartingButton.GetComponent<Button>().onClick.AddListener(BasicDialogue);

        button1.transform.GetChild(0).GetComponent<Text>().text = button1Name;
        button1.GetComponent<Button>().onClick.AddListener(Button1);

        button2.transform.GetChild(0).GetComponent<Text>().text = button2Name;
        button2.GetComponent<Button>().onClick.AddListener(Button2);

        button3.transform.GetChild(0).GetComponent<Text>().text = button3Name;
        button3.GetComponent<Button>().onClick.AddListener(Button3);

        button4.transform.GetChild(0).GetComponent<Text>().text = button4Name;
        button4.GetComponent<Button>().onClick.AddListener(Button4);

        button5.transform.GetChild(0).GetComponent<Text>().text = button5Name;
        button5.GetComponent<Button>().onClick.AddListener(Button5);

        button6.transform.GetChild(0).GetComponent<Text>().text = button6Name;
        button6.GetComponent<Button>().onClick.AddListener(Button6);

        button7.transform.GetChild(0).GetComponent<Text>().text = button7Name;
        button7.GetComponent<Button>().onClick.AddListener(Button7);

        button8.transform.GetChild(0).GetComponent<Text>().text = button8Name;
        button8.GetComponent<Button>().onClick.AddListener(Button8);

    }
    public void BasicDialogue()
    {
        TriggerDialogue(dialogue);
    }
    public void Button1()
    {
        TriggerDialogue(WatchDialogue);
    }
    public void Button2()
    {
        TriggerDialogue(NameTagDialogue);
    }
    public void Button3()
    {
        TriggerDialogue(ShoePrintDialogue);
    }
    public void Button4()
    {
        TriggerDialogue(HatDialogue);
    }
    public void Button5()
    {
        TriggerDialogue(FlashLightDialogue);
    }
    public void Button6()
    {
        TriggerDialogue(LunchBoxDialogue);
    }
    public void Button7()
    {
        TriggerDialogue(MoustacheDialogue);
    }
    public void Button8()
    {
        TriggerDialogue(GlassCutDialogue);
    }
}
