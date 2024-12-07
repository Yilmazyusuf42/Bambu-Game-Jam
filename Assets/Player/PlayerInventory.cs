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

    public void UpdateInventoryUI()
    {
        if (InventoryUIImage != null)
        {
            InventoryUIImage.gameObject.SetActive(Inventory);
        }
    }

    //public Sprite InventoryImage;

    public Image InventoryUIImage;

    private void Update()
    {
       // InventoryUIImage.sprite = InventoryImage;
    }


}
