 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ChControllerMoving : MonoBehaviour
{
    [SerializeField] private float playerVelocity = 10f, jumpForce = 50f;

    private Vector3 direction = Vector3.zero;
    private CharacterController controller;

    private float gratityForce;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        isGrounded = controller.isGrounded;
        
        MoveLogic();
        GravityLogic();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveLogic()
    {
        direction.x = Input.GetAxis("Horizontal") * playerVelocity;
        direction.z = Input.GetAxis("Vertical") * playerVelocity;

        direction.y = gratityForce;

        direction = transform.TransformDirection(direction);
        controller.Move(direction * Time.deltaTime);
    }

    void GravityLogic()
    {
       

        if (!isGrounded)
            gratityForce -= 20f * Time.deltaTime;

        else
            gratityForce = -1f;

        if (Input.GetAxis("Jump") > 0 && isGrounded)
            gratityForce = jumpForce;   
    }
}
