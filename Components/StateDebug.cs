using Godot;
using System;

public partial class StateDebug : Label
{
	// Called when the node enters the scene tree for the first time.
	[Export] CharacterStateMachine characterStateMachine;
	public override void _Ready()
	{
		characterStateMachine = this.Owner.GetNode<CharacterStateMachine>("CharacterStateMachine");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Text = $"State: {characterStateMachine.CurrentState.ToString()}";
	}
}
