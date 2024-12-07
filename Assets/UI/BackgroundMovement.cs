using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeedPercentage = 1f;

    private Camera mainCam;
    private Vector2 initalCamPos;
    private Vector2 initialBackgroundPos;



    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        initalCamPos = mainCam.transform.position;
        initialBackgroundPos = transform.position;
    }

    private void Update()
    {
        Vector2 currentCamPos = mainCam.transform.position;
        float distance = (initalCamPos.x - currentCamPos.x) * moveSpeedPercentage;

        float distanceBackgroundToTravel = initialBackgroundPos.x - distance;

        transform.position = new Vector2(distanceBackgroundToTravel, transform.position.y);
    }
}
