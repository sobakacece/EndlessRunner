using Godot;
using System;

public partial class AirState : State
{
	[Export] State groundState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		groundState = MyStateMachine.GetNode<State>("Ground");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void StateProcess(double delta)
	{
		Landing();
	}
	private void Landing()
	{
		if (MyCharacter.IsOnFloor())
		nextState = groundState;
	}
}
