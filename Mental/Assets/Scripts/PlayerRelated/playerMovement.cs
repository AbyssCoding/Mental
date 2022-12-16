using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //Declares all variables and GameObjects
    private Vector2 MouseInput;
    [SerializeField] private float Sensitivity;
    [Space]
    [SerializeField] private float stamina;
    [SerializeField] private float drainRate = 1f;
    [SerializeField] private float rechargeRate = 1f;
    [Space]
    [SerializeField] bool isRunning;
    [SerializeField] private float sprint;
    [SerializeField] private float walk;
    [Space]
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float jumpforce;
    public bool isGrounded;
    public Vector3 playermovementInput;
    [Space]
    [SerializeField] private Rigidbody r_Player;
    [SerializeField] private Animator a_Player;

    public void OnCollisionStay(Collision collision)


    {
        isGrounded = true;
    }
    // Checks if the player is on the ground
    public void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
    private void sprinting()
    {
        //Checks if the control key is pressed and stamina is not zero
        if (Input.GetKeyDown(KeyCode.LeftControl) && (stamina > 1f))
        {
            Speed = sprint;
            isRunning = true;

        }
        //executes if stamina is zero or the control key isn't pressed
        if (Input.GetKeyUp(KeyCode.LeftControl) || stamina <= 0f)
        {
            Speed = walk;
            isRunning = false;


        }
    }

    // Update is called once per frame
    void Update()
    {
        //Defines movement and mouse inputs
        playermovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        MouseInput = new Vector2(0f, Input.GetAxis("Mouse X") * Sensitivity);

        MovePlayer();
        
        AnimatePlayer();
        //Regenerates or drains player stamina
        if (isRunning)
        {
            stamina -= Time.deltaTime * drainRate;
        }
        if (!isRunning)
        {
            stamina += Time.deltaTime * rechargeRate;
        }
        //Keeps stamina within specific parameters
        if (stamina > 10)
        {
            stamina = 10f;
        }
        if (stamina < 0f)
        {
            stamina = 0f;
        }

    }
    void MovePlayer()
    {
        sprinting();
        //Physically moves the player
        Vector3 moveVector = transform.TransformDirection(playermovementInput) * Speed;
        r_Player.velocity = new Vector3(moveVector.x, r_Player.velocity.y, moveVector.z);
        //causes player to jump on space key input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == (true))
        {
            r_Player.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    private void RotatePlayer()
    {
        //Rotates player on mouse cursor position
        if (playermovementInput == Vector3.zero)
        {
            Sensitivity = 0;
        }
        else
        {
            Sensitivity = 7;
        }
        transform.Rotate(MouseInput);
    }

    private void AnimatePlayer()
    {
        if(playermovementInput != Vector3.zero)
        {
            a_Player.Play("walkcycle");
            a_Player.Play("SkirtSway");
            a_Player.SetBool("isWalking", true);
        }
        else
        {
            a_Player.SetBool("isWalking", false);
        }
    }
}