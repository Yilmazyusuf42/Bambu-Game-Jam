using System;
using UnityEngine;

public class CarDriveTriggerSystem : TriggerSystem
{
    public static Action OnPlayerStartedToDrive;
    public static Action OnPlayerStoppedToDrive;
    [SerializeField] private GameObject getOutFromCar;

    private bool isDriving;
    private TheCar car;

    private void Awake()
    {
        car = GetComponentInParent<TheCar>();   
    }

    protected override void Start()
    {
        base.Start();
        getOutFromCar.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnPlayerStartedToDrive?.Invoke();
            uiElement.SetActive(false);
            getOutFromCar.SetActive(true);
            isDriving = true;
        }

        if (isPlayerInTrigger && isDriving && Input.GetKeyDown(KeyCode.Alpha2) && car.IsHandBreakActive())
        {
            OnPlayerStoppedToDrive?.Invoke();
            getOutFromCar.SetActive(false);
            uiElement.SetActive(true);
            isDriving = false;

        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
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

    protected override void OnTriggerExit2D(Collider2D other)
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
