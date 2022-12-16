using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliders : MonoBehaviour
{
    public List<GameObject> colList;
    public List<GameObject> filteredColList;

    private void OnTriggerEnter(Collider other)
    {
        colList.Add(other.gameObject);
        if (other.tag == "Player")
        {
            if (other.name == "Hittbox")
            {
                filteredColList.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        colList.Remove(other.gameObject);
        if(filteredColList.Contains(other.gameObject))
        {
            filteredColList.Remove(other.gameObject);
        }
    }
}
