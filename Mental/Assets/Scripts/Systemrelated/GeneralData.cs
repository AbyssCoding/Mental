using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralData : MonoBehaviour
{
    public int Health;
    public int Currency;
    public int Experience;
    public Vector3 Position;
    public Quaternion Rotation;
    public string serialNumber;


    private void Start()
    {
        if(this.gameObject.tag == "Player")
        {
            serialNumber = "PLAYER";
        }
    }
    private void Update()
    {
        Position = this.transform.position;
        Rotation = this.transform.rotation;
        if(this.gameObject.tag == "Enemy")
        {
            Health = this.GetComponent<Experimental>().health;
        }
        if(Health == 0 && this.gameObject.tag == "Enemy")
        {
            this.GetComponent<EnemyStateManager>().isDead = true;
            Debug.Log(this.gameObject.name + " is Dead");
        }
        else if(Health == 0 && this.gameObject.tag == "Player")
        {
            //End Game Behavior
        }
       
    }
}
