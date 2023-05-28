using Godot;
using System;

public partial class TreeSpawner : Spawner
{
    [Export] int minRange = 400;
    [Export] int maxRange = 1000;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // MyHeight = -60;
        MySpawnRange = this.rnd.Next(minRange, maxRange);
        MyPath = "res://Spawners/tree_spawner.tscn";
        GD.Print($"{this.Name} has spawnrange {MySpawnRange}");
        base._Ready();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
