using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject objShouldBeVisibleFromInside;
    [SerializeField] private GameObject objShouldBeVisibleFromOutside;


    private void EnterBuilding()
    {
        objShouldBeVisibleFromInside.SetActive(true);
        objShouldBeVisibleFromOutside.SetActive(false);
    }

    private void ExitBuilding()
    {
        objShouldBeVisibleFromInside.SetActive(false);
        objShouldBeVisibleFromOutside.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Car"))
        {
            EnterBuilding();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Car"))
        {
            ExitBuilding();
        }
    }

}
