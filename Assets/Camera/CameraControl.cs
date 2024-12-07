using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraControl : MonoBehaviour
{

    [Header("Target Settings")]
    public Transform target;

    [Header("Camera Movement")]
    [Range(0f, 10f)]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("Camera Bounds")]
    public float minX = float.MinValue;
    public float maxX = float.MaxValue;
    public float minY = float.MinValue;
    public float maxY = float.MaxValue;

    [Header("Tracking Mode")]
    public bool horizontalOnlyTracking = false;
    public bool verticalOnlyTracking = false;

    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        desiredPosition = target.position + offset;

        if (horizontalOnlyTracking)
        {
            desiredPosition.y = transform.position.y;
        }

        if (verticalOnlyTracking)
        {
            desiredPosition.x = transform.position.x;
        }

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
