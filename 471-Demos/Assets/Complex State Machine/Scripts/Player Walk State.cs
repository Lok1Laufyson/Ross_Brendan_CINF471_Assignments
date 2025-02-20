using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(Player_State_Manager player)
    {
        Debug.Log("I'm walking!");
    }

    public override void UpdateState(Player_State_Manager player)
    {
        //What are we doing during this state?
        player.MovePlayer(player.speed);

        //On what conditions do we leave the state?
        if (player.movement.magnitude < 0.1)
        {
            player.SwitchState(player.idleState);
        }   

        if (player.crouching)
        {
            player.SwitchState(player.sneakState);
        }
    }
}
