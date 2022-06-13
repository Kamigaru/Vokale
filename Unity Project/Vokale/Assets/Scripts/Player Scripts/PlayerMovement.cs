using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController player;

    public float speed = 10f;
    public float sprintSpeed = 30f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGround;

    // Update is called once per frame
    void Update()
    {

        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //Pressing Shift to Sprint
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            player.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            player.Move(move * speed * Time.deltaTime);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        //Gravity
        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);

    }
}