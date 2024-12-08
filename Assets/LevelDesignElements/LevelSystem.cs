using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;

    //public List<int> cameraBounders;

    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> backgroundObjects;

    int maxLevel = 4;

    public int currentLevel = 1; // Baþlangýç seviyesi

    void Start()
    {
        //UpdateLevelObjects();
    }

    /*
    public void callWhileLevelChange()
    {
        UpdateLevelObjects();
    }
    */

    public void SetLevel(int level)
    {
        if (level < 1)
        {
            Debug.Log("Minimum seviye 1'dir. Seviyeyi 1'e ayarlýyorum.");
            level = 1;
        }
        else if (level > backgroundObjects.Count)
        {
            Debug.Log($"Maksimum seviye {backgroundObjects.Count}'dir. Seviyeyi maksimuma ayarlýyorum.");
            level = backgroundObjects.Count;
        }

        currentLevel = level;
        Debug.Log($"Seviye {currentLevel} olarak ayarlandý.");
        //UpdateLevelObjects();
        SetActiveLevelObjects();
    }

    private void SetActiveLevelObjects()
    {
        for (int i = 0; i < maxLevel; i++)
        {
            // Ana objenin tüm çocuklarýný kapat
            foreach (Transform child in backgroundObjects[i].transform)
            {
                child.gameObject.SetActive(false);
            }

            // Sadece aktif seviyeye denk gelen objenin tüm çocuklarýný aç
            if (i == currentLevel - 1)
            {
                Debug.Log("Çocuðun birini açtý : " + backgroundObjects[i]);
                foreach (Transform child in backgroundObjects[i].transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }


    // Level'a göre GameObject'leri güncelleme
    private void UpdateLevelObjects()
    {
        for (int i = 0; i < maxLevel; i++)
        {
            backgroundObjects[i].SetActive(i == currentLevel - 1);

        }
    }
}
