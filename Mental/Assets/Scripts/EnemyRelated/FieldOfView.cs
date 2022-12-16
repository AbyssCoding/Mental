using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 40 ;
    [Range(0, 360)]
    public float viewAngle;
    public float alertedRadius;
    [Range(0, 360)]
    public float alertedAngle;
    public bool playerSeen;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public Transform Player;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        StartCoroutine("FindTargetWithDelay", .2f);
    }
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private void Update()
    {
        if(visibleTargets.Contains(Player))
        {
            playerSeen = true;
            Debug.Log("Player is Seen");
            viewRadius = alertedRadius;
            viewAngle = alertedAngle;
        }
        if(!visibleTargets.Contains(Player))
        {
     
                playerSeen = false;
                viewRadius = 25f;
                viewAngle = 160;
        }
    }
}
