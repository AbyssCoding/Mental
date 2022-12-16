using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData 
{
    public int health;
    public int currency;
    public float[] position;
    public string serialNumber;


    public EntityData (GeneralData data)
    {
        health = data.Health;
        currency = data.Currency;
        serialNumber = data.serialNumber;

        position = new float[3];
        position[0] = data.Position.x;
        position[1] = data.Position.y;
        position[2] = data.Position.z;
    }
}
