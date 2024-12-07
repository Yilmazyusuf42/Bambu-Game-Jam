using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private bool collectable = false;
    private GameObject playerObject;

    [SerializeField] private Sprite itemSprite; // Ýtemin sprite'ý (Görseli)

    private void Start()
    {
        itemSprite = GetComponent<SpriteRenderer>().sprite;    
    }

    private void Update()
    {
        if (collectable && Input.GetKeyDown(KeyCode.E) && !PlayerInventory.instance.Inventory)
        {
            if (playerObject != null)
            {
                PlayerInventory.instance.Inventory = true;
                PlayerInventory.instance.UpdateInventoryUI(itemSprite);
                
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // Oyuncu layer'ýný kontrol et
        {
            Debug.Log("Oyuncu yakýnlaþtý");
            playerObject = other.gameObject;
            collectable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerObject = null;
            collectable = false;
        }
    }
}
