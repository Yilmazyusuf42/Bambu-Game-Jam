using UnityEngine;

public class Wheel : MonoBehaviour
{
    public Rigidbody2D carRigidbody; // Aracýn Rigidbody2D bileþeni
    public float rotationMultiplier = 10f; // Dönüþ hýzýný kontrol eden çarpan

    public float suspensionStrength = 1000f; // Süspansiyonun yay kuvveti
    public float suspensionDamping = 50f; // Amortisör kuvveti
    public float maxSuspensionHeight = 1f; // Süspansiyonun maksimum sýkýþma mesafesi
    public float wheelRadius = 0.5f; // Tekerlek yarýçapý
    public LayerMask groundLayer; // Zemin katmaný

    private float suspensionCompression = 0f; // Süspansiyonun sýkýþma miktarý
    private float previousHeight = 0f; // Önceki yerden yükseklik deðeri

    private void LateUpdate()
    {
        // Aracýn hýzýný al
        float carSpeed = carRigidbody.velocity.x;

        // Dönüþ hýzýný hesapla
        float rotationSpeed = carSpeed * rotationMultiplier;

        // Tekerleði döndür
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

        // Süspansiyon sistemini hesapla
        HandleSuspension();
    }

    void HandleSuspension()
    {
        // Zeminle olan mesafeyi ölçmek için raycast kullan
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, wheelRadius + 0.5f, groundLayer);
        if (hit.collider != null)
        {
            float currentHeight = hit.distance; // Zeminle arasýndaki mesafe

            // Süspansiyonun sýkýþma miktarýný hesapla
            float compression = Mathf.Clamp01((currentHeight - wheelRadius) / maxSuspensionHeight);
            suspensionCompression = Mathf.Lerp(suspensionCompression, compression, Time.deltaTime * suspensionDamping);

            // Süspansiyon kuvveti uygula (yay kuvv.eti)
            float springForce = suspensionStrength * suspensionCompression;
            Vector2 suspensionForce = new Vector2(0, -springForce);

            // Amortisör kuvvetini uygula (sýkýþma ile hýzýn çarpýmý)
            Vector2 dampingForce = new Vector2(0, -suspensionDamping * (carRigidbody.velocity.y));

            // Uygulanan toplam kuvveti aracýn Rigidbody2D'sine ekle
            carRigidbody.AddForce(suspensionForce + dampingForce);
        }
    }
}
