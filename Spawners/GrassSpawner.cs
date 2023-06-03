using Godot;
using System;
using System.Linq;

public partial class GrassSpawner : Spawner
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        // MyHeight = this.rnd.Next(minHeigth, maxHeight);
        MyPath = "res://Spawners/GrassSpawner.tscn";
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
