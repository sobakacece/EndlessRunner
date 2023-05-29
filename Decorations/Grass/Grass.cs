using Godot;
using System;

public partial class Grass : Decoration
{
    private float height = -10;
    private int rangeOfRandom = 0;
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
    }

    public override void Despawn()
    {
		// GD.Print("Grass despawned");
        base.Despawn();
    }
}
