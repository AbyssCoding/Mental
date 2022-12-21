using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;

    public CinemachineFreeLook rig;
    public CharacterController controller;

    public Animator animator;

    public Transform groundCheck;
    public float groundDist = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight;
    public float gravityScale;

    public float speed;

    public float turnSmoothTime = 0.1f;

    public float stamina;
    public float drainRate;
    public float rechargeRate;

    float turnSmooothVelocity;

    public float radius;
    public float maxRadius;
    public float minRadius;
    public float zoomRate;


    Vector3 Velocity;
    float gravity = -9.81f;
    void Start()
    {
        stamina = 10;
        radius = maxRadius/2;
        
    }

    // Update is called once per frame
    void Update()
    {
        rig.m_Orbits[0].m_Radius = radius;
        rig.m_Orbits[1].m_Radius = radius;
        rig.m_Orbits[2].m_Radius = radius;
        rig.m_Orbits[0].m_Height = radius;
        rig.m_Orbits[2].m_Height = -1f * radius;

        radius -= zoomRate * Input.mouseScrollDelta.y;

        if(radius > maxRadius)
        {
            radius = maxRadius;
        }
        if(radius < minRadius)
        {
            radius = minRadius;
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if(isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2;
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity * gravityScale);
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            animator.Play("Walking");
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.Play("Idle");
            animator.SetBool("isWalking", false);
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        Sprinting();

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);
    }


    void Sprinting()
    {
        if(Input.GetKey(KeyCode.LeftControl) && stamina > 0)
        {
            speed = 15;
            stamina -= drainRate * Time.deltaTime;

        }
        if(!Input.GetKey(KeyCode.LeftControl) || stamina == 0)
        {
            speed = 10;
            stamina += drainRate * Time.deltaTime;
        }

        if(stamina > 10)
        {
            stamina = 10;
        }
        if(stamina < 0)
        {
            stamina = 0;
        }
    }
}
