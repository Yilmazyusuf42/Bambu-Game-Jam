using System;
using UnityEngine;

public class FuelTriggerSystem : TriggerSystem
{
    public static Action<int> OnFuelGained;

    private int fuelAmountToGain = 500;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && PlayerInventory.instance.Inventory && isPlayerInTrigger)
        {
            OnFuelGained?.Invoke(fuelAmountToGain);
            PlayerInventory.instance.Inventory = false;
            PlayerInventory.instance.InventoryUIImage.gameObject.SetActive(false);
            uiElement.SetActive(false);
        }
    }



    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isPlayerInTrigger = true;
            if (uiElement != null && PlayerInventory.instance.Inventory)
            {
                uiElement.SetActive(true);
            }

        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isPlayerInTrigger = false;
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }
    }
}
