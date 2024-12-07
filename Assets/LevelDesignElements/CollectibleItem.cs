using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    bool collectable =false;

    private GameObject playerObject;

    private void Start()
    {

    }

    void Update()
    {
        if (collectable && Input.GetKeyDown(KeyCode.E) && !PlayerInventory.Inventory)
        {
            if (playerObject != null)
            {
                PlayerInventory.Inventory = true;
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("Oyuncuuu");

            playerObject = other.gameObject;
            collectable = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            playerObject = null;
            collectable = false;
        }
    }
}
