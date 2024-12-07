using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRegion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            TheCar.aracYurur -= 20;

            collision.GetComponent<EnemyHealth>().Die();
        }
    }
}
