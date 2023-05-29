using Godot;
using System;
using System.Linq;

public partial class CloudSpawner : Spawner
{
    [Export] float speed = 200;
    [Export] int minHeigth = -500, maxHeight = -100;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        foreach (Decoration child in this.GetChildren().OfType<Decoration>())
        {
            MyHeight = rnd.Next((int)child.MyHeight - child.RangeOfRandom / 2, (int)child.MyHeight + child.RangeOfRandom / 2);
        }
        // MyHeight = this.rnd.Next(minHeigth, maxHeight);
        MyPath = "res://Spawners/cloud_spawner.tscn";
    }

    public override void _Process(double delta)
    {
        if (player.Velocity.X < 0)
        {
            this.GlobalPosition += (player.Velocity.X + speed) * (float)delta * Vector2.Right;
        }
        else if (player.Velocity.X > 0)
        {
        this.GlobalPosition += Vector2.Right * (player.Velocity.X - speed) * (float)delta;
        }
        else
        {
            this.GlobalPosition += Vector2.Left * speed * (float)delta;
        }
    

    }
}
