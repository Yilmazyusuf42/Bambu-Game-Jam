using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public bool inventoryFull = false; // Envanterin dolu olup olmadýðýný kontrol eder


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýþan nesne "Player" layer'ýndaysa ve envanter boþsa
        if (collision.gameObject.layer == 15 && !inventoryFull)
        {
            inventoryFull = true; // Envanteri dolu olarak iþaretle
            Debug.Log("Item toplandý. Envanter dolu!");

            // Bu itemi sahneden kaldýr
            Destroy(gameObject);
        }
    }
}
