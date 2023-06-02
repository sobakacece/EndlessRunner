using Godot;
using System;

public partial class CreditsScreen : GameScreen
{
	public GameScreen MyParent {get => MyGlobalSettings.MyOptions;}
	Button returnButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		returnButton = GetNode<Button>("Overlay/VBoxContainer/VBoxContainer/BackButton");
		returnButton.Connect("pressed", new Callable(this, "Return"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void Return()
	{
		ChangeScreen(MyParent);
	}
}
