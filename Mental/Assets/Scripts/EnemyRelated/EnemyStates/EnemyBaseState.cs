using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager Enemy);

    public abstract void UpdateState(EnemyStateManager Enemy);

    public abstract void OnCollisionEnter(EnemyStateManager Enemy, Collision collider);

    public abstract void OnTriggerEnter(EnemyStateManager Enemy, Collider collider);

    public abstract void OnCollisionExit(EnemyStateManager Enemy, Collision collider);

    public abstract void OnCollisionStay(EnemyStateManager Enemy, Collision collider);

}
