using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    public float angleX;
    public float angleY;
    public float radius = 10;
    public float Sensitivity;
    private Vector3 offset;
    private Vector3 Origin;

    private void Start()
    {
        Origin = transform.position;
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;
    }
    private void Update()
    {
        angleX += Input.GetAxis("Mouse X") * Sensitivity;
        angleY = Mathf.Clamp(angleY -= Input.GetAxis("Mouse Y") * Sensitivity, -89, 0);
        radius = Mathf.Clamp(radius -= Input.mouseScrollDelta.y, 1, 10);

        if (angleX > 360)
        {
            angleX -= 360;
        }
        else if (angleX < 0)
        {
            angleX += 360;
        }

        Vector3 orbit = Vector3.forward * radius;
        orbit = Quaternion.Euler(angleY, angleX, 0) * orbit;

        transform.position = player.transform.position + orbit;
        transform.LookAt(player.transform.position);

        if(player.GetComponent<playerMovement>().playermovementInput != Vector3.zero)
        {
           
        }
        if(player.GetComponent<playerMovement>().playermovementInput == Vector3.zero) 
        {
            
        }
    }
}