using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Motor : MonoBehaviour
{
    CharacterController playerCharacter;
    Vector3 move_direction;

    public float moveSpeed = 10.0f;
    public float gravity = 10.0f;

    public float jump_force = 10.0f;
    public float vetical_velocity;

    void Awake()
    {
        playerCharacter = GetComponent<CharacterController>();   
    }

    void Start()
    {
        
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (playerCharacter.isGrounded)
        {
            move_direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
            move_direction *= moveSpeed;

            if (Input.GetButton(Axis.JUMP))
            {
                move_direction.y = jump_force;
            }
        }
        
        move_direction.y -= gravity;

        playerCharacter.Move(move_direction * Time.deltaTime);
    }
}
