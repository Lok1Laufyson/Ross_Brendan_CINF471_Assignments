using UnityEngine;

public class PlayerSneakState : PlayerBaseState
{
    public override void EnterState(Player_State_Manager player)
    {
        Debug.Log("I'm sneaking!");
    }

    public override void UpdateState(Player_State_Manager player)
    {
        float sneak_speed = player.speed * 0.5f;
        player.MovePlayer(sneak_speed);

        //What are we doing during this state?
        player.MovePlayer(player.speed);

        //On what conditions do we leave the state?
        if (player.movement.magnitude < 0.1)
        {
            player.SwitchState(player.idleState);
        }

        if (player.crouching == false)
        {
            player.SwitchState(player.walkState);
        }
    }
}
