using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private void Awake()
    {
        instance = this;
    }


    public bool Inventory = false;


    //public Sprite InventoryImage;

    public Image InventoryUIImage;

    private void Update()
    {
       // InventoryUIImage.sprite = InventoryImage;
    }


}
