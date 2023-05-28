using Godot;
using System;

public partial class GameOverScreen : GameScreen
{
    [Export] protected Label scoreLabel;

    public override void _Ready()
    {
        ConnectToNodes();
        scoreLabel.Text = $"Your Score: {signalBus.FinalScore.ToString("0000000")}";

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }




}
