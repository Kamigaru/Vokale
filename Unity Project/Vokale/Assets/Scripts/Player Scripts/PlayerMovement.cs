using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController player;

    public GameObject playerEyes;

    [Header("Interactable Settings")]
    public LayerMask interactables;
    [Range(1f, 5f)]
    public float interactDistance;

    [Header("Movement Settings")]
    public float speed = 10f;
    public float sprintSpeed = 30f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;

    [Header("Ground Checker")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGround;

    public RenderTerrainChunkInDistance instance;

    private void Awake()
    {
        instance = GameObject.Find("TerrainChunks").GetComponent<RenderTerrainChunkInDistance>();
    }

    private void Start()
    {
        instance.CheckShouldRender(transform);
    }

    // Update is called once per frame
    void Update()
    {

        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Physics.Raycast(playerEyes.transform.position, playerEyes.transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, interactDistance))
        {
            Debug.DrawLine(playerEyes.transform.position, playerEyes.transform.TransformDirection(Vector3.forward) * interactDistance, Color.green);
            IHarvestable harvest = hitinfo.collider.gameObject.GetComponent<Tree>();
            if(harvest != null && Input.GetMouseButton(0))
            {
                harvest.Interact();
            }
        }

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
            instance.CheckShouldRender(gameObject.transform);
        }
        else
        {
            player.Move(move * speed * Time.deltaTime);
            instance.CheckShouldRender(transform);
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