using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;

    public int xoffset;

    //public List<int> cameraBounders;

    private void Awake()
    {
        instance = this;
    }

    GameObject Backgrounds;

    //public List<GameObject> backgroundObjects;

    int maxLevel = 4;

    public int currentLevel = 1; // Baþlangýç seviyesi

    void Start()
    {
        //UpdateLevelObjects();
        Backgrounds = this.gameObject;

        maxLevel = Backgrounds.transform.childCount;
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
        else if (level > maxLevel)
        {
            level = maxLevel;
        }

        currentLevel = level;

        SetActiveLevelObjects();
    }

    private void SetActiveLevelObjects()
    {
        foreach (Transform child in Backgrounds.transform)
        {
            child.gameObject.SetActive(false);
        }


        Backgrounds.transform.GetChild(currentLevel - 1).gameObject.SetActive(true);
        Backgrounds.transform.GetChild(currentLevel - 1).gameObject.transform.position = new Vector3(Backgrounds.transform.GetChild(currentLevel - 1).gameObject.transform.position.x + xoffset, Backgrounds.transform.GetChild(currentLevel - 1).gameObject.transform.position.y);


    }


}
