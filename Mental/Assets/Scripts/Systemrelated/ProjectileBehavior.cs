using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float existTimer;

    public bool useGravity;

    public int damageDealt;

    public Rigidbody self;


    private void Awake()
    {
        
        self = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(EndOfLifeClause(existTimer, self.gameObject));

        if (useGravity)
        {
            self.useGravity = true;
        }
        if (!useGravity)
        {
            self.useGravity = false;
        }

    }
    private IEnumerator EndOfLifeClause(float Length, GameObject Client)
    {
        yield return new WaitForSeconds(Length);
        Destroy(Client);

    }
    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Hey! We hit!!");
            collision.transform.root.GetComponent<GeneralData>().Health -= damageDealt;
            Destroy(this.gameObject);
        }
        if( collision.gameObject.tag == "Enemy")
        {
            //Do absolutely nothing
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}