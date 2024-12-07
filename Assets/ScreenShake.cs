using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;
    [SerializeField] private float duration = .2f;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        Instance = this;

        originalPosition = transform.position;
    }



    public void Shake(float xMagnitude, float yMagnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        shakeCoroutine = StartCoroutine(ShakeCoroutine(xMagnitude,yMagnitude));
    }

    private IEnumerator ShakeCoroutine(float xMagnitude, float yMagnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * xMagnitude;
            float offsetY = Random.Range(-1f, 1f) * yMagnitude;

            transform.position = transform.position + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}

