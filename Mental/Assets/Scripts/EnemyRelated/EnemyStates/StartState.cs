using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager Enemy)
    {
        Debug.Log("Here We go!");
        Enemy.stateReader = "Start State";
    }
    public override void UpdateState(EnemyStateManager Enemy)
    {
        
    }
    public override void OnCollisionEnter(EnemyStateManager Enemy, Collision collider)
    {
        
    }
    public override void OnTriggerEnter(EnemyStateManager Enemy, Collider collider)
    {
        
    }
    public override void OnCollisionExit(EnemyStateManager Enemy, Collision collider)
    {
        if (collider.transform.gameObject.tag == "Ground")
        {
            Enemy.SwitchState(Enemy.patrolState);
        }
    }
    public override void OnCollisionStay(EnemyStateManager Enemy, Collision collider)
    {
        if(collider.transform.gameObject.tag == "Ground")
        {
            Enemy.gameObject.transform.Translate(Vector3.up * 5);
            Debug.Log("Currently In Ground");
        }
    }
}
