using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected bool isKnockedBack;
    protected float knockBackDuration=.25f;
    private IEnumerator KnockbackRoutine(Vector3 direction, float forceMagnitude)
    {

        isKnockedBack = true;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction * forceMagnitude;

        // Ensure we reset the position to the exact end point
        while (elapsedTime < knockBackDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / knockBackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(waitingTimeAfterKnockBack);
        isKnockedBack = false;
    }

    public void KnockBack(Vector3 hitPoint, float forceMagnitude)
    {
        if (!isKnockedBack)
        {
            Vector3 forceDirection = transform.position - hitPoint;
            forceDirection.y = 0f;
            //?rb.AddForce(forceDirection.normalized, ForceMode.Impulse);
            StartCoroutine(KnockbackRoutine(forceDirection, forceMagnitude));
        }
    }
}
