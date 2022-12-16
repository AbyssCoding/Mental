using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    public bool leftClicking;
    public bool rightClicking;
    public bool middleClicking;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            leftClicking = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rightClicking = true;
        }
        else if (Input.GetMouseButtonDown(2))
        {
            middleClicking = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            leftClicking = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightClicking = false;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            middleClicking = false;
        }
    }
}
