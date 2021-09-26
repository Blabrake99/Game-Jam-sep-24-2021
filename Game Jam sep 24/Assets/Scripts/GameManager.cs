using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("The slots for the inventory")]
    GameObject[] InventorySlots = new GameObject[12];

    [HideInInspector] public bool hasWatch, hasNametag, hasShoePrint, hasHat, hasFlashLight, hasLunchBox,
        hasMoustache, hasGlassCut;

    [SerializeField] GameObject[] DialogueButtons = new GameObject[8];

    [HideInInspector] public int InventorySpacesUsed = 0;

    [SerializeField] GameObject ButtonPage;

    public bool StartWinning;

    public bool StartLosing;

    [SerializeField, Tooltip("The item description for this object in Inventory")]
    string BloodDescription;

    [SerializeField, Tooltip("The Image for this object in the inventory")]
    Sprite BloodItemSprite;
    bool hasBloodHintAlready;
    MainCamera camera;
    private void Start()
    {
        camera = FindObjectOfType<MainCamera>();
        lockCursor();
    }

    //gonna change later

    private void Update()
    {
        if (hasWatch)
        {
            if(!DialogueButtons[0].activeSelf)
                DialogueButtons[0].SetActive(true);
        }
        if (hasNametag)
        {
            if (!DialogueButtons[1].activeSelf)
                DialogueButtons[1].SetActive(true);
        }
        if (hasShoePrint)
        {
            if (!DialogueButtons[2].activeSelf)
                DialogueButtons[2].SetActive(true);
        }
        if (hasHat)
        {
            if (!DialogueButtons[3].activeSelf)
                DialogueButtons[3].SetActive(true);
        }
        if (hasFlashLight)
        {
            if (!DialogueButtons[4].activeSelf)
                DialogueButtons[4].SetActive(true);
        }
        if (hasLunchBox)
        {
            if (!DialogueButtons[5].activeSelf)
                DialogueButtons[5].SetActive(true);
        }
        if (hasMoustache)
        {
            if (!DialogueButtons[6].activeSelf)
                DialogueButtons[6].SetActive(true);
        }
        if (hasGlassCut)
        {
            if (!DialogueButtons[7].activeSelf)
                DialogueButtons[7].SetActive(true);
        }

    }
    public void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void UpdateBloodInventory()
    {
        if (InventorySpacesUsed < InventorySlots.Length && !hasBloodHintAlready)
        {
            InventorySlots[InventorySpacesUsed].GetComponent<InventorySlots>().ItemDescription = BloodDescription;
            InventorySlots[InventorySpacesUsed].GetComponent<Image>().sprite = BloodItemSprite;
            hasGlassCut = true;
            hasBloodHintAlready = true;
            InventorySpacesUsed += 1;
        }
    }
    public void UpdateInventory(string InventoryParagraph, Sprite image)
    {
        if(InventorySpacesUsed < InventorySlots.Length)
        {
            InventorySlots[InventorySpacesUsed].GetComponent<InventorySlots>().ItemDescription = InventoryParagraph;
            InventorySlots[InventorySpacesUsed].GetComponent<Image>().sprite = image;
            InventorySpacesUsed += 1;
        }
    }
    public void CloseDialogueBox()
    {
        TalkibleNPC[] temps = FindObjectsOfType<TalkibleNPC>();

        foreach(TalkibleNPC t in temps)
        {
            t.InMenu = false;
        }
        camera.CanMoveCamera = true;
        ButtonPage.SetActive(false);
        lockCursor();
    }
    public void DisableButton(int index)
    {
        DialogueButtons[index].GetComponent<Button>().interactable = false;
    }
    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
