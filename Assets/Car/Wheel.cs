using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public Rigidbody2D carRigidbody; // Aracýn Rigidbody2D bileþeni
    public float rotationMultiplier = 10f; // Dönüþ hýzýný kontrol eden çarpan

    private void LateUpdate()
    {
        // Aracýn hýzýný al
        float carSpeed = carRigidbody.velocity.x;

        // Dönüþ hýzýný hesapla
        float rotationSpeed = carSpeed * rotationMultiplier;



        // Tekerleði döndür
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}
