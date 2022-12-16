using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    float timer;
    public override void EnterState(EnemyStateManager Enemy)
    {
        Enemy.stateReader = "AttackState";
        timer = 0;
    }
    public override void UpdateState(EnemyStateManager Enemy)
    {
        if(Enemy.Colliders.Count != 0)
        {
            Regulator(Enemy);
        }
        Enemy.StartCoroutine(FollowCycle(Enemy));
        
    }
    public override void OnCollisionEnter(EnemyStateManager Enemy, Collision collider)
    {

    }
    public override void OnTriggerEnter(EnemyStateManager Enemy, Collider collider)
    {

    }
    public override void OnCollisionExit(EnemyStateManager Enemy, Collision collider)
    {

    }

    public override void OnCollisionStay(EnemyStateManager Enemy, Collision collider)
    {

    }
   
    IEnumerator FollowCycle(EnemyStateManager Guy)
    {
        Guy.navMesh.destination = Guy.Player.position;
        yield return new WaitForSeconds(0.5f);
    }

    private void Regulator(EnemyStateManager Guy)
    {
        timer = new float();
        timer += Time.deltaTime;
        if(timer == Guy.attackDelay)
        {
            Guy.Colliders[0].transform.root.GetComponent<GeneralData>().Health -= Guy.Damage;
            Debug.Log("Enemy Hit");
            timer = 0;
        }
    }

}
