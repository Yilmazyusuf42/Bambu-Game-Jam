using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public int changeLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            Debug.Log("Allooooo");
            LevelSystem.instance.SetLevel(changeLevel);

        }
    }
}
