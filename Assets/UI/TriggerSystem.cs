using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    public GameObject uiElement; // UI panelini buraya baðlayýn
    //public GameObject playerCar; // Oyuncu arabasýný buraya baðlayýn


    bool isPlayerInTrigger = false; // Oyuncunun trigger içinde olup olmadýðýný takip eder

    void Start()
    {
        // UI baþlangýçta kapalý
        if (uiElement != null)
            uiElement.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8) // Oyuncuyu tespit etmek için Tag kontrolü
        {
            isPlayerInTrigger = true;
            if (uiElement != null)
                uiElement.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isPlayerInTrigger = false;
            if (uiElement != null)
                uiElement.SetActive(false);
        }
    }


}
