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

    public bool talking = false;

    DialogueManager manager;

    float Timer;

    [SerializeField] GameObject ButtonPage;

    [SerializeField] GameObject StartingButton, WatchBtn, NameTagBtn, ShoePrintBtn, HatBtn, FlashLightBtn, LunchBoxBtn, MoustacheBtn, GlassCutBtn;

    GameManager Gm;

    [SerializeField] TalkingTypes[] talkingTypes = new TalkingTypes[9];

    TalkingTypes CurrentType;
    [HideInInspector]
    public bool Question1Unlocked,
        Question2Unlocked, Question3Unlocked;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        ButtonPage.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        manager = FindObjectOfType<DialogueManager>();
        Gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < Talkdistance)
        {
            if (Input.GetButtonDown("Interact") && !talking && Timer <= 0)
            {
                Gm.unlockCursor();
                setButtons();
                ButtonPage.SetActive(true);
                Timer = .5f;
            }
            if (Input.GetButtonDown("Interact") && ButtonPage.activeSelf && Timer <= 0)
            {
                Gm.lockCursor();
                ButtonPage.SetActive(false);
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
        StopAnimation();
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
        ResetButtons();
        FindAnimation();
        ButtonPage.SetActive(false);
        talking = true;
        manager.StartDialogue(log);
        manager.NPC = this.gameObject;
        UnsetButtons();
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
        StartingButton.SetActive(true);
        StartingButton.GetComponent<Button>().onClick.AddListener(BasicDialogue);

        WatchBtn.GetComponent<Button>().onClick.AddListener(Watch);

        NameTagBtn.GetComponent<Button>().onClick.AddListener(NameTag);

        ShoePrintBtn.GetComponent<Button>().onClick.AddListener(ShoePrint);

        HatBtn.GetComponent<Button>().onClick.AddListener(Hat);

        FlashLightBtn.GetComponent<Button>().onClick.AddListener(FlashLight);

        LunchBoxBtn.GetComponent<Button>().onClick.AddListener(LunchBox);

        MoustacheBtn.GetComponent<Button>().onClick.AddListener(Moustache);

        GlassCutBtn.GetComponent<Button>().onClick.AddListener(GlassCut);

    }
    void UnsetButtons()
    {
        //this ready's up all the buttons to start dialogue 
        StartingButton.SetActive(true);
        StartingButton.GetComponent<Button>().onClick.RemoveListener(BasicDialogue);

        WatchBtn.GetComponent<Button>().onClick.RemoveListener(Watch);

        NameTagBtn.GetComponent<Button>().onClick.RemoveListener(NameTag);

        ShoePrintBtn.GetComponent<Button>().onClick.RemoveListener(ShoePrint);

        HatBtn.GetComponent<Button>().onClick.RemoveListener(Hat);

        FlashLightBtn.GetComponent<Button>().onClick.RemoveListener(FlashLight);

        LunchBoxBtn.GetComponent<Button>().onClick.RemoveListener(LunchBox);

        MoustacheBtn.GetComponent<Button>().onClick.RemoveListener(Moustache);

        GlassCutBtn.GetComponent<Button>().onClick.RemoveListener(GlassCut);

    }
    void FindAnimation()
    {
        if (CurrentType == TalkingTypes.Normal)
        {
            anim.SetBool("IsTalking", true);
        }
        if (CurrentType == TalkingTypes.Suprised)
        {
            anim.SetBool("IsSuprised", true);
        }
        if (CurrentType == TalkingTypes.HesOnToMe)
        {
            anim.SetBool("IsOnToMe", true);
        }
        if (CurrentType == TalkingTypes.Angry)
        {
            anim.SetBool("IsAngry", true);
        }
    }
    void StopAnimation()
    {

        anim.SetBool("IsTalking", false);

        anim.SetBool("IsSuprised", false);

        anim.SetBool("IsOnToMe", false);

        anim.SetBool("IsAngry", false);

    }
    enum TalkingTypes
    {
        Normal,
        Angry,
        HesOnToMe,
        Suprised
    }
    public void BasicDialogue()
    {
        CurrentType = talkingTypes[0];
        TriggerDialogue(dialogue);
    }
    public void Watch()
    {
        CurrentType = talkingTypes[1];
        TriggerDialogue(WatchDialogue);
    }
    public void NameTag()
    {
        CurrentType = talkingTypes[2];
        TriggerDialogue(NameTagDialogue);
    }
    public void ShoePrint()
    {
        CurrentType = talkingTypes[3];
        TriggerDialogue(ShoePrintDialogue);
    }
    public void Hat()
    {
        CurrentType = talkingTypes[4];
        TriggerDialogue(HatDialogue);
    }
    public void FlashLight()
    {
        CurrentType = talkingTypes[5];
        TriggerDialogue(FlashLightDialogue);
    }
    public void LunchBox()
    {
        CurrentType = talkingTypes[6];
        TriggerDialogue(LunchBoxDialogue);
    }
    public void Moustache()
    {
        CurrentType = talkingTypes[7];
        TriggerDialogue(MoustacheDialogue);
    }
    public void GlassCut()
    {
        CurrentType = talkingTypes[8];
        TriggerDialogue(GlassCutDialogue);
    }
    void ResetButtons()
    {
        StartingButton.GetComponent<Button>().interactable = true;
        WatchBtn.GetComponent<Button>().interactable = true;
        NameTagBtn.GetComponent<Button>().interactable = true;
        ShoePrintBtn.GetComponent<Button>().interactable = true;
        HatBtn.GetComponent<Button>().interactable = true;
        FlashLightBtn.GetComponent<Button>().interactable = true;
        LunchBoxBtn.GetComponent<Button>().interactable = true;
        MoustacheBtn.GetComponent<Button>().interactable = true;
        GlassCutBtn.GetComponent<Button>().interactable = true;
    }
}
