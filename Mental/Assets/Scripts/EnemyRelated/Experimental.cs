using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Experimental : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Transform Sector;

    public string SectorName;

    public float WanderDistance;

    public LayerMask whatIsGround, whatIsPlayer;

    public int health;

    public FieldOfView fov;

    private Vector3 lookAt;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenRangedAttacks;
    public float timeBetweenMeleeAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public bool RangedAttackOnly;
    public bool MeleeAttackOnly;
    public bool UsingRanged;
    public int meleeDamage;
    public float meleeRange;
    public float rangedRange;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        Sector = GameObject.Find(SectorName).transform;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        if(RangedAttackOnly && !MeleeAttackOnly)
        {
            UsingRanged = true;
        }
        if(MeleeAttackOnly && !RangedAttackOnly)
        {
            UsingRanged = false;
        }
        if(RangedAttackOnly && MeleeAttackOnly)
        {
            Debug.LogError("You cannot have both Melee and Ranged only options selected");
        }
        if(!RangedAttackOnly && !MeleeAttackOnly)
        {
            int decision = Random.Range(0, 1);
            if(decision == 0)
            {
                UsingRanged = false;
                attackRange = meleeRange;
            }
            if (decision == 1)
            {
                UsingRanged = true;
                attackRange = rangedRange;
            }
        }
        if(!UsingRanged)
        {
            attackRange = meleeRange;
        }
        if(UsingRanged)
        {
            attackRange = rangedRange;
        }
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange =  fov.playerSeen;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
        

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        
        float distance = Vector3.Distance(Sector.transform.position, walkPoint);
        if (distance > WanderDistance)
        {
            walkPoint = Sector.transform.position;
            Debug.Log("Recentering");
        }
        if(distance < WanderDistance || distance <= 0 )
        {
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
             
        }

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Debug.Log("Chasing Player");
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(lookAt);

        if (!alreadyAttacked)
        {
            ///Attack code here
           if(UsingRanged)
            {
                
                Rigidbody rb = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
                Debug.Log("Fired");
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 6f, ForceMode.Impulse);
                

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenRangedAttacks);
            }
           if(!UsingRanged)
            {
                player.GetComponent<GeneralData>().Health -= meleeDamage;
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), (timeBetweenMeleeAttacks * 0.1f));
            }
            ///End of attack code
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangedRange);
        
    }
}
