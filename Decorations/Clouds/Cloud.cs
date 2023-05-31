using Godot;
using System;

public partial class Cloud : Decoration
{

    private float height = -300;
    private int rangeOfRandom = 300;
    [Export] public override float MyHeight { get => height; set => height = value; }
    [Export] public override int RangeOfRandom { get => rangeOfRandom; set => rangeOfRandom = value; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ConnectToNotifier();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Vector2 position = Position;
        // position.X = Mathf.MoveToward(GlobalPosition.X, Vector2.Right.X * speed, player.Velocity.X);
        // Position = position;
        // GD.Print("CloudSpawned");

    }
    public override void Despawn()
    {
        base.Despawn();
    }

}
