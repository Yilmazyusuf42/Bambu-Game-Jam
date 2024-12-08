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
            print("ARABAya BÝNDÝ");
            uiElement.SetActive(false);
            getOutFromCar.SetActive(true);
            isDriving = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("2'YE BASILDI");
            if (isDriving)
            {
                print("IS PLAYER IN TRIGGER" + isPlayerInTrigger);
                if (car.IsHandBreakActive())
                {
                    OnPlayerStoppedToDrive?.Invoke();
                    print("ARABADAN ÝNDÝ");
                    getOutFromCar.SetActive(false);
                    uiElement.SetActive(true);
                    isDriving = false;
                }
            }
        }

        //if (isPlayerInTrigger && isDriving && Input.GetKeyDown(KeyCode.Alpha2) && car.IsHandBreakActive())
        //{
        //    OnPlayerStoppedToDrive?.Invoke();
        //    print("ARABADAN ÝNDÝ");
        //    getOutFromCar.SetActive(false);
        //    uiElement.SetActive(true);
        //    isDriving = false;

        //}
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && !isPlayerInTrigger)
        {
            print("in trigger true oldu");
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

            print("in trigger false oldu");
            isPlayerInTrigger = false;
            if (uiElement != null)
            {
                uiElement.SetActive(false);
                
            }
        }
    }
}
