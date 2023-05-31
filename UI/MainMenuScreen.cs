using Godot;
using System;

public partial class MainMenuScreen : GameScreen
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ConnectToNodes();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void ConnectToNodes()
    {
        restartButton.Connect("pressed", new Callable(this, "LoadGame"));
        quitButton.Connect("pressed", new Callable(this, "Quit"));
        signalBus = GetNode<SignalBus>("/root/SignalBus");

    }
    public override void LoadGame()
    {
		this.Visible = false;
        base.LoadGame();
    }

}
