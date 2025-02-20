using UnityEngine;
using UnityEngine.InputSystem;

public class Player_State_Manager : MonoBehaviour
{

    public PlayerBaseState currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerSneakState sneakState = new PlayerSneakState();


    public Vector2 movement;
    CharacterController controller;
    public bool crouching = false;
    public float speed = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        SwitchState(idleState);

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    //Handle Input//

    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void OnSprint(InputValue crouchVal)
    {
        if (crouching == false)
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }
    }

    // Helper Functions//
    public void MovePlayer(float speed)
    {
        float moveX = movement.x;
        float moveY = movement.y;

        Vector3 actual_movement = new Vector3(moveX, 0, moveY);
        controller.Move(actual_movement * Time.deltaTime);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState = newState;
        currentState.UpdateState(this);
    }
}
