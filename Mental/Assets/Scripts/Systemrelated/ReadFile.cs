using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class ReadFile : MonoBehaviour
{
    public TextAsset dataFile;
    public string[] datalines;
    public GameObject Player;

    public GameObject obj1;
    public GameObject obj2;

    public GameObject sec1;
    public GameObject sec2;
    public GameObject sec3;
    public GameObject sec4;


    GameObject Prefab;
    Vector3 spawnPoints;

    public List<string[]> data;



    private void Start()
    {
        datalines = dataFile.text.Split("\n");


        foreach (string line in datalines)
        {
            string ID = line.Substring(0, 5);
            string sector = line.Substring(5, 2);
            string health = line.Substring(7, 3);
            float healthPercentage = (float)Convert.ToInt32(health);

            GameObject Enemy = Instantiate(TranslateObject(ID), TranslateArea(sector), Quaternion.identity);
            if(Enemy.GetComponent<GeneralData>() != null)
            {
                Enemy.GetComponent<GeneralData>().serialNumber = line;
            }

        }
    }

    public GameObject TranslateObject(string serialNumber)
    {
        if (serialNumber == "ABCDE")
        {
            return  obj1;
        }
        else if (serialNumber == "FGHIJ")
        {
            return obj2;
        }
       
        else
        {
            Debug.LogError("Object With Serial Number " + serialNumber + " does not exist");
            return null;
        }
    }
    public Vector3 TranslateArea(string serialNumber)
    {
        if (serialNumber == "01")
        {
            return UnityEngine.Random.insideUnitSphere * 6 + new Vector3(sec1.transform.GetChild(sec1.transform.childCount - 1).transform.position.x, sec1.transform.GetChild(sec1.transform.childCount - 1).transform.position.y + 12, sec1.transform.GetChild(sec1.transform.childCount - 1).transform.position.z);


        }
        else if (serialNumber == "02")
        {
            return UnityEngine.Random.insideUnitSphere * 6 + new Vector3(sec2.transform.GetChild(sec2.transform.childCount - 1).transform.position.x, sec2.transform.GetChild(sec2.transform.childCount - 1).transform.position.y + 12, sec2.transform.GetChild(sec2.transform.childCount - 1).transform.position.z);

        }
        else if (serialNumber == "03")
        {
            return UnityEngine.Random.insideUnitSphere * 6 + new Vector3(sec3.transform.GetChild(sec3.transform.childCount - 1).transform.position.x, sec3.transform.GetChild(sec3.transform.childCount - 1).transform.position.y + 12, sec3.transform.GetChild(sec3.transform.childCount - 1).transform.position.z);
        }
        else if (serialNumber == "04")
        {
            return UnityEngine.Random.insideUnitSphere * 6 + new Vector3(sec4.transform.GetChild(sec4.transform.childCount - 1).transform.position.x, sec4.transform.GetChild(sec4.transform.childCount - 1).transform.position.y + 12, sec4.transform.GetChild(sec4.transform.childCount - 1).transform.position.z);
        }
        else
        {
            Debug.LogError("Sector number " + serialNumber + " does not exist");
            return (Vector3.zero);
            
        }
    }
}