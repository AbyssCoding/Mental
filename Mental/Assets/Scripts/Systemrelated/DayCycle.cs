using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float daySpeed;
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, (daySpeed * 0.001f), 0);
        
    }
}
