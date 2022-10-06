using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharContr : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 playerMovementInput;
    
    [SerializeField] private CharacterController controller;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float dash;
    [SerializeField] private float jumpforce;
    [SerializeField] private float sensitivity;
    [SerializeField] private float gravity = -9.81f;



    // Update is called once per frame
    void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();

    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput);

        if (controller.isGrounded)
        {
            velocity.y = -1;
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        controller.Move(moveVector * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }


}
