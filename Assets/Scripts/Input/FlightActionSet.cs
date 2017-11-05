using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class FlightActionSet : PlayerActionSet {

    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Thrust;
    public PlayerAction Pause;

    public FlightActionSet() : base() {
        Left = CreatePlayerAction("Left");
        Right = CreatePlayerAction("Right");
        Thrust = CreatePlayerAction("Thrust");
        Pause = CreatePlayerAction("Pause");
    }

    public static FlightActionSet CreateWithDefaultBindings() {
        var playerActions = new FlightActionSet();
        playerActions.Left.AddDefaultBinding(Key.A);
        playerActions.Left.AddDefaultBinding(Key.LeftArrow);
        playerActions.Right.AddDefaultBinding(Key.D);
        playerActions.Right.AddDefaultBinding(Key.RightArrow);
        playerActions.Thrust.AddDefaultBinding(Key.W);
        playerActions.Thrust.AddDefaultBinding(Key.Space);
        playerActions.Thrust.AddDefaultBinding(Key.UpArrow);
        playerActions.Pause.AddDefaultBinding(Key.Escape);
        InputManager.AttachPlayerActionSet(playerActions);
        return playerActions;
    }
}