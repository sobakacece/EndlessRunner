using Godot;
using System;
using System.Linq;

public partial class RocketSpawner : Spawner
{
    [Export] int minRange = 400;
    [Export] int maxRange = 600;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (Rocket child in this.GetChildren().OfType<Rocket>())
		{
			this.MyHeight = child.Position.Y + 10;
		}
        MySpawnRange = this.rnd.Next(minRange, maxRange);
        MyPath = "res://Spawners/RocketSpawner.tscn";
        // GD.Print($"{this.Name} has spawnrange {MySpawnRange}");
        base._Ready();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
