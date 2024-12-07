using System;
using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    [SerializeField] protected GameObject uiElement;

    protected bool isPlayerInTrigger = false;

    protected virtual void Start()
    {
        if (uiElement != null)
            uiElement.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isPlayerInTrigger = true;
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }

        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
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
