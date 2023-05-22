using Godot;
using System;

public partial class GameOverScreen : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.
    [Export] Button restartButton, quitButton;
    [Export] Label scoreLabel;
    SignalBus signalBus;
    Player player;

    public override void _Ready()
    {
        restartButton.Connect("pressed", new Callable(this, "Restart"));
        quitButton.Connect("pressed", new Callable(this, "Quit"));
        signalBus = GetNode<SignalBus>("/root/SignalBus");
        scoreLabel.Text = $"Your Score: {signalBus.FinalScore.ToString("0000000")}";

        // signalBus.SaveScore += ChangeScore;
        // signalBus.PlayerEnteredScene += ParsePlayer;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public void Quit()
    {
        GetTree().Quit();
    }
    public void Restart()
    {
        GetTree().ChangeSceneToFile("res://Levels/test_level.tscn");
        GetTree().Paused = false;
    }
}
