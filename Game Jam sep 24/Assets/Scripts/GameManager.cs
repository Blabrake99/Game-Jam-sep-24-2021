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

    private void Start()
    {
        lockCursor();
    }

    //gonna change later

    //private void Update()
    //{
    //    if(hasWatch)
    //    { 
        
    //    }
    //    if(hasNametag)
    //    { 
        
    //    }
    //    if(hasShoePrint)
    //    { 
        
    //    }
    //    if(hasHat)
    //    {
        
    //    }
    //    if(hasFlashLight)
    //    { 
        
    //    }
    //    if(hasLunchBox)
    //    {
        
    //    }
    //    if(hasMoustache)
    //    {
        
    //    }
    //    if (hasGlassCut)
    //    {

    //    }

    //}
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
}
