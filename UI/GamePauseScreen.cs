using Godot;
using System;

public partial class GamePauseScreen : GameScreen
{
    [Export] Button continueButton;

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
        continueButton.Connect("pressed", new Callable(this, "Continue"));
        base.ConnectToNodes();
    }
    public void Continue()
    {
        GetTree().Paused = false;
       Visible = false;
       QueueFree();
    }
    //  public override void _Input(InputEvent @event)
    // {
    //     if (@event.IsActionPressed("pause"))
    //     {
    //        Continue();
    //     }
    // }
}
