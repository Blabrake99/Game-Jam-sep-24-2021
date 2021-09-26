using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Queue<string> sentences;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text dialogueText;
    [SerializeField]
    GameObject textPanal;

    [HideInInspector]
    public GameObject NPC;

    GameObject GM;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }
    //this starts the dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        GM.GetComponent<GameManager>().unlockCursor();
        textPanal.SetActive(true);
        sentences.Clear();
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    //put this on a continue button or if you press a button
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    //this slowly writes the text
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.02f);
        }
    }
    //this ends the dialogue
    public void EndDialogue()
    {
        dialogueText.text = "";
        nameText.text = "";
        GM.GetComponent<GameManager>().lockCursor();
        textPanal.SetActive(false);
        if(NPC.GetComponent<TalkibleNPC>())
            NPC.GetComponent<TalkibleNPC>().DoneTalking();
        if (NPC.GetComponent<PickUpables>())
            NPC.GetComponent<PickUpables>().DoneTalking();
        if (NPC.GetComponent<Interactables>())
            NPC.GetComponent<Interactables>().DoneTalking();
        if (NPC.GetComponent<PoliceNPC>())
            NPC.GetComponent<PoliceNPC>().DoneTalking();
        NPC = null;    
    }
}
