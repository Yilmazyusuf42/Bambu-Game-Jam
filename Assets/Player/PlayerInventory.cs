using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    public Image InventoryUIImage;

    public bool Inventory = false;

    private void Awake()
    {
        instance = this;
    }




    public void UpdateInventoryUI(Sprite sprite)
    {
        if (InventoryUIImage != null)
        {
            InventoryUIImage.gameObject.SetActive(Inventory);
            InventoryUIImage.sprite = sprite;
            print("BURAYA GÝRDÝ");
        }
    }

    //public Sprite InventoryImage;



    private void Update()
    {
       // InventoryUIImage.sprite = InventoryImage;
    }


}
