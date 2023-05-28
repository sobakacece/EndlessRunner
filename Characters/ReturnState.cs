using Godot;
using System;

public partial class ReturnState : State
{
	Vector2 returnVelocity;
	State groundState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		returnVelocity = new Vector2 (-MyCharacter.speed, 0);
		MyCharacter.Velocity = Vector2.Zero;
		groundState = MyStateMachine.stateList["Ground"];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MyCharacter.Velocity = MyCharacter.Velocity.Lerp(new Vector2 (MyCharacter.speed, 0), 1);
		if (MyCharacter.Velocity >= returnVelocity)
		{
			nextState = groundState;
		}
	}
}
