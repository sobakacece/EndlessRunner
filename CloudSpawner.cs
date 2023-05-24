using Godot;
using System;

public partial class CloudSpawner : Spawner
{
	[Export] float speed = 10;
	[Export] int minHeigth = -500, maxHeight = -100;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MyHeight = this.rnd.Next(minHeigth, maxHeight);
		MyPath = "res://Spawners/cloud_spawner.tscn";
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.GlobalPosition += new Vector2 (speed, 0);
	}
}
