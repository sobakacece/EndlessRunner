using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class Spawner : Node2D
{
    [Export] public float MyHeight { get; set; }
    [Export] public float MySpawnRange { get; set; }
	public float SpawnPoint { get; set; }
	private Player player;
    public List<Node2D> MyScenes;
    public Random rnd = new Random();
    public string MyPath { get; set; }
    public void PickRandom()
    {
        int random = rnd.Next(0, MyScenes.Count);
        for (int i = 0; i < MyScenes.Count; i++)
        {
            if (i != random)
                this.RemoveChild(MyScenes[i]);
        }
    }

    public override void _Ready()
    {
        // GD.Print("Ready");
		player = (Player)GetNode("/root/TestLevel/Player");
        MyScenes = new List<Node2D>();
        foreach (Node2D child in this.GetChildren().OfType<Node2D>())
        {
            MyScenes.Add(child);
        }
        PickRandom();
    }


    public override void _Process(double delta)
    {
    }
}
