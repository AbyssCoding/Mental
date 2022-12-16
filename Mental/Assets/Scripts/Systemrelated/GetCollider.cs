using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCollider : MonoBehaviour
{
    public List<GameObject> colList;
    public List<GameObject> filteredColList;

    private void OnTriggerEnter(Collider other)
    {
        colList.Add(other.gameObject);
        if(other.tag == "Enemy")
        {
            if(other.name == "Hitbox")
            {
                filteredColList.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        colList.Remove(other.gameObject);
        if (filteredColList.Contains(other.gameObject))
        {
            filteredColList.Remove(other.gameObject);
        }
    }
    
}
