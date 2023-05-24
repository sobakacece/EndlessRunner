using Godot;
using System;

public partial class TreeSpawner : Spawner
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MyHeight = -60;
        MySpawnRange = 400;
        MyPath = "res://Spawners/tree_spawner.tscn";
        base._Ready();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
