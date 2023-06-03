using Godot;
using System;

public partial class TreeSpawner : Spawner
{
    int minRange = 200;
    int maxRange = 350;

    [Export] int MyMinRange { get => minRange; set => minRange = value; }
    [Export] int MyMaxRange { get => maxRange; set => maxRange = value; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // MyHeight = -60;
        MySpawnRange = this.rnd.Next(minRange, maxRange);
        MyPath = "res://Spawners/TreeSpawner.tscn";
        // GD.Print($"{this.Name} has spawnrange {MySpawnRange}");
        base._Ready();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
