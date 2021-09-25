using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Start()
    {
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
        ButtonPage.SetActive(false);
        lockCursor();
    }
}
