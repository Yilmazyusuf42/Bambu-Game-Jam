using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public bool inventoryFull = false; // Envanterin dolu olup olmadýðýný kontrol eder
    public int playerLayer; // Oyuncunun layer'ýný buraya tanýmla

    private void Start()
    {
        // Oyuncu layer'ýný bir kez belirle (örneðin "Player" layer'ý 8. sýradaysa bunu Unity'den ayarlamalýsýn)
        playerLayer = LayerMask.NameToLayer("Items");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
