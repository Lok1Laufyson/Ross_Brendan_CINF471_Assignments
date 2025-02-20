using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(Player_State_Manager player)
    {
        Debug.Log("I'm idling!");
    }

    public override void UpdateState(Player_State_Manager player)
    {
        //What are we doing during this state?
        //Nothing!

        //On what conditions do we leave the state?
        if (player.movement.magnitude > 0.1)
        {
            if (player.crouching == false)
            {
                player.SwitchState(player.walkState);
            }
            else if (player.crouching == true)
            {
                player.SwitchState(player.sneakState);
            }
        }
    }
}
