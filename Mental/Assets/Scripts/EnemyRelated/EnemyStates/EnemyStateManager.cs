using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public PatrolState patrolState = new PatrolState();
    public StartState startState = new StartState();
    public DespawnState despawnState = new DespawnState();
    public DeathState deathState = new DeathState();
    public AttackState attackState = new AttackState();
    public Transform hurtBox;
    public Transform HitBox;
    public List<GameObject> Colliders;
    public GameObject[] Sectors;
    public NavMeshAgent navMesh;
    public bool playerSeen;
    public Transform Player;
    public bool isDead;
    public string stateReader;
    public int Damage;
    public int attackDelay;
    public bool CanHit;
    public float count;



    // Start is called before the first frame update
    void Start()
    {
        hurtBox = this.transform.Find("Hurtbox");
        HitBox = this.transform.Find("Hitbox");
        currentState = startState;
        Player = GameObject.Find("Player").transform;

        currentState.EnterState(this);
        navMesh = GetComponent<NavMeshAgent>();
        Sectors = GameObject.FindGameObjectsWithTag("Sector");
    }

    void Update()
    {
        currentState.UpdateState(this);
        playerSeen = GetComponent<FieldOfView>().playerSeen;
        Colliders = hurtBox.GetComponent<EnemyColliders>().filteredColList;
    }

   public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }
    private void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(this, collision);
    }
    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        currentState.OnCollisionStay(this, collision);
    }
    public void StartAttackCycle(IEnumerator Coroutine)
    {
        StartCoroutine(Coroutine);
    }
    
}
