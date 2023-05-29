using Godot;
using System;

public partial class SignalBus : Node
{
    [Signal] public delegate void HealthChangedEventHandler(Node node, int health_changed);
    [Signal] public delegate void RestartEventHandler();
    // [Signal] public delegate void PlayerEnteredSceneEventHandler(Player player, int health);
    // [Signal] public delegate void GameOverEventHandler(int score);
    public float FinalScore { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.s
    public override void _Process(double delta)
    {
    }




}

