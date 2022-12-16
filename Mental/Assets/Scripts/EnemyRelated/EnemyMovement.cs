using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float seekTimer;
    [SerializeField] private int Selector;
    [SerializeField] public NavMeshAgent navMesh;
    [SerializeField] private GameObject Destination;
    [SerializeField] private int Counter;
    [SerializeField] public List<GameObject> possibleDestinations;
    [Space]
    [SerializeField] public bool playerSeen;
    [SerializeField] public GameObject Player;
    bool execute;
    private Vector3 targetPostition;
    public bool signal;
    public bool lockedOn;
    public int Speed;
    public bool ambush;
    [SerializeField] private Transform lookAt;

    private void Start()
    {
        for (int i = 0; i < possibleDestinations.Count; i++)
        {
            GameObject temp = possibleDestinations[i];
            int randomIndex = Random.Range(i, possibleDestinations.Count);
            possibleDestinations[i] = possibleDestinations[randomIndex];
            possibleDestinations[randomIndex] = temp;
        }
        navMesh = GetComponent<NavMeshAgent>();
        Counter = 0;
        navMesh.destination = possibleDestinations[Counter].transform.position;
        Destination = possibleDestinations[Counter];


    }



    private void Update()
    {
        playerSeen = GetComponent<FieldOfView>().playerSeen;
        targetPostition = new Vector3(Player.transform.position.x,
                                        this.transform.position.y,
                                        Player.transform.position.z);
        if (playerSeen == true)
        {


            navMesh.destination = Player.transform.position;
            Destination = Player;
            lockedOn = true;
            execute = false;
        }
        if (playerSeen == false)
        {
            if (ambush)
            {
                transform.LookAt(lookAt);
            }
            if (!execute)
            {
                StartCoroutine(Reset(seekTimer));
                execute = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerSeen == false)
        {
            if (other.gameObject.tag == ("Waypoint"))
            {
                if (playerSeen == false)
                {
                    Counter += 1;
                    if (Counter > possibleDestinations.Count - 1)
                    {
                        Counter = 0;
                    }
                    Destination = possibleDestinations[Counter];

                    navMesh.destination = possibleDestinations[Counter].transform.position;

                }
            }

        }

    }
    IEnumerator Reset(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        navMesh.destination = possibleDestinations[Counter].transform.position;
        Destination = possibleDestinations[Counter];
        signal = true;
        lockedOn = false;
    }

}

