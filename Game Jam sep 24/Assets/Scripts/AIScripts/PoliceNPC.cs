using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PoliceNPC : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue NoneRightDialogue;
    public Dialogue OneRightDialogue;
    public Dialogue TwoRightDialogue;

    GameObject player;

    [SerializeField, Tooltip("Set this if Ways == GetClose")]
    float Talkdistance;

    bool talking = false;

    DialogueManager manager;

    float Timer;

    [SerializeField] GameObject ButtonPage;
    [SerializeField] GameObject GuessPage;

    [SerializeField] GameObject StartingButton, WatchBtn, NameTagBtn, ShoePrintBtn, HatBtn, FlashLightBtn, LunchBoxBtn, MoustacheBtn, GlassCutBtn;

    GameManager Gm;

    [HideInInspector]
    public bool Question1Unlocked,
        Question2Unlocked, Question3Unlocked;

    int rightGuesses;

    int wrongGuess;

    int guesses;

    bool ChoosedRight;
    bool ChoosedWrong;

    bool TalkedToOnce;

    private void Start()
    {
        ButtonPage.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        manager = FindObjectOfType<DialogueManager>();
        Gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < Talkdistance)
        {
            if(!TalkedToOnce && Input.GetButtonDown("Interact") && !talking && Timer <= 0)
            {
                TriggerDialogue(dialogue);
                Timer = .5f;
                TalkedToOnce = true;
            }
            if (Input.GetButtonDown("Interact") && !talking && Timer <= 0 && TalkedToOnce)
            {
                Gm.unlockCursor();
                setButtons();
                ButtonPage.SetActive(false);
                GuessPage.SetActive(true);
                Timer = .5f;
            }
            if (Input.GetButtonDown("Interact") && ButtonPage.activeSelf && Timer <= 0)
            {
                Gm.lockCursor();
                ButtonPage.SetActive(false);
                GuessPage.SetActive(false);
                Timer = .5f;
            }
            //this is a way for the player to skip dialogue if he's talking to the npc
            if (Input.GetButtonDown("Pause") && talking)
            {
                Gm.lockCursor();
                ButtonPage.SetActive(false);
                GuessPage.SetActive(false);
                EndDialogue();
            }
            if (Input.GetButtonDown("Pause"))
            {
                Gm.lockCursor();
                ButtonPage.SetActive(false);
                GuessPage.SetActive(false);
            }
            if(guesses == 2)
            {
                if(rightGuesses == 2)
                {
                    TriggerDialogue(TwoRightDialogue);
                }
                if(rightGuesses == 1 && wrongGuess == 1)
                {
                    TriggerDialogue(OneRightDialogue);
                }
                if(wrongGuess == 2)
                {
                    TriggerDialogue(NoneRightDialogue);
                }
                ChoosedRight = false;
                ChoosedWrong = false;
                rightGuesses = 0;
                wrongGuess = 0;
                guesses = 0;
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
        //ButtonPage.SetActive(false);
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
        ResetButtons();
        //this ready's up all the buttons to start dialogue 
        StartingButton.SetActive(false);
        StartingButton.GetComponent<Button>().onClick.AddListener(BasicDialogue);

        WatchBtn.GetComponent<Button>().onClick.AddListener(RightButton);

        NameTagBtn.GetComponent<Button>().onClick.AddListener(WrongButton);

        ShoePrintBtn.GetComponent<Button>().onClick.AddListener(WrongButton);

        HatBtn.GetComponent<Button>().onClick.AddListener(WrongButton);

        FlashLightBtn.GetComponent<Button>().onClick.AddListener(WrongButton);

        LunchBoxBtn.GetComponent<Button>().onClick.AddListener(WrongButton);

        MoustacheBtn.GetComponent<Button>().onClick.AddListener(RightButton);

        GlassCutBtn.GetComponent<Button>().onClick.AddListener(WrongButton);

    }
    void RightButton()
    {
        rightGuesses += 1;
        guesses += 1;
    }
    void WrongButton()
    {
        wrongGuess += 1;
        guesses += 1;
    }
    public void ChooseRight()
    {
        ChoosedRight = true;
        ButtonPage.SetActive(true);
        GuessPage.SetActive(false);
    }
    public void ChooseWrong()
    {
        ChoosedWrong = true;
        ButtonPage.SetActive(true);
        GuessPage.SetActive(false);
    }
    void ResetButtons()
    {
        WatchBtn.GetComponent<Button>().interactable = true;
        NameTagBtn.GetComponent<Button>().interactable = true;
        ShoePrintBtn.GetComponent<Button>().interactable = true;
        HatBtn.GetComponent<Button>().interactable = true;
        FlashLightBtn.GetComponent<Button>().interactable = true;
        LunchBoxBtn.GetComponent<Button>().interactable = true;
        MoustacheBtn.GetComponent<Button>().interactable = true;
        GlassCutBtn.GetComponent<Button>().interactable = true;
    }
    public void BasicDialogue()
    {
        TriggerDialogue(dialogue);
    }
}
