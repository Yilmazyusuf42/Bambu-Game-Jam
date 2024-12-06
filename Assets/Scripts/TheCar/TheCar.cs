using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TheCar : MonoBehaviour
{
    [Header(" GameObjects ")]
    [SerializeField] private GameObject taret;
    [SerializeField] private GameObject missle;
    [SerializeField] private Transform misslePlace;
    [SerializeField] private float fireStandBy = .1f;
    float fireTimer;

    [SerializeField] private float gear1Speed;
    [SerializeField] private float gear2Speed;


    int gearLevel = 0;
    float speed;
    Vector3 mousePos;
    bool controllingTaret;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        Movement();

        if (Input.GetKeyDown(KeyCode.W))
            gearUp();

        if (Input.GetKeyDown(KeyCode.S))
            gearDown();


        if (Input.GetKeyDown(KeyCode.E))
            TaretOnOff();



        ControlTheTaret();
        FiringTheMissle();
    }

    private void FiringTheMissle()
    {
        if (controllingTaret)
        {
            if (Input.GetMouseButton(0) && fireTimer < 0)
            {
                Missle newMissle = Instantiate(missle, misslePlace.position, Quaternion.identity).GetComponent<Missle>();
                newMissle.AdjustTheMissle(mousePos);
                fireTimer = fireStandBy;
            }
        }
    }







    // Controlling the taret with mouse
    public void ControlTheTaret()
    {
        if (controllingTaret)
        {
            Vector3 mousePos = takeMousePos();
            // Objeye doğru bir vektör oluştur 
            Vector3 direction = mousePos - transform.position;
            // Rotasyonu hesapla 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Objeyi döndür
            taret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }


    private void TaretOnOff()
    {
        if (controllingTaret == false)
            controllingTaret = true;
        else if (controllingTaret == true)
            controllingTaret = false;
    }


    private Vector3 takeMousePos()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        return mousePos;
    }



    // Velocity of the Car
    private void Movement()
    {
        if (gearLevel > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }



    #region  Gear
    // Aracın fitesine göre hızını değiştirildiği kısım
    private void gearLevelDetection()
    {
        Debug.Log($"vites {gearLevel}");
        if (gearLevel == 0)
            speed = 0f;
        else if (gearLevel == 1)
            speed = gear1Speed;
        else if (gearLevel == 2)
            speed = gear2Speed;
    }

    public void gearUp()
    {
        if (gearLevel < 2)
            gearLevel++;
        gearLevelDetection();
    }

    public void gearDown()
    {
        if (gearLevel > 0)
            gearLevel--;
        gearLevelDetection();
    }

    #endregion
}
