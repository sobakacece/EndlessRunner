using Godot;
using System;

public partial class GroundState : State
{
	[Export] public float jumpVelocity;
	[Export] State airState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		airState = MyStateMachine.GetNode<State>("Air");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void StateInput(InputEvent @event)
	{
		if (@event.IsActionPressed("jump"))
		{
			Jump();
		}
	}
	public override void OnEnter()
	{
		playback.Travel("walk");
	}
	    public void Jump()
    {
		MyCharacter.Velocity = new Vector2(0, jumpVelocity);
        playback.Travel("jump");
		nextState = airState;
    }
}
