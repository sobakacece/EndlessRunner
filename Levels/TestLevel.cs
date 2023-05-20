using Godot;
using System;
using System.Collections.Generic;

public partial class TestLevel : Node2D
{
    Player player;
    TileMap tileMap;
    List<PackedScene> obstaclesScenes;
    float spawnPoint;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = this.GetNode<Player>("Player");
        tileMap = this.GetNode<TileMap>("TileMap");
        spawnPoint = player.GlobalPosition.X;
        obstaclesScenes = GetAllSpawnable("res://Obstacles/");

    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        tileMap.GlobalPosition = new Vector2(player.GlobalPosition.X, tileMap.GlobalPosition.Y);
        // SpawnObject(100);
		SpawnObjectAtEndOfScreen();
    }
    public List<PackedScene> GetAllSpawnable(string path)
    {
        List<PackedScene> pathes = new List<PackedScene>();
        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string filename = dir.GetNext();
            while (filename != "")
            {
                pathes.Add((PackedScene)ResourceLoader.Load(path + filename));
                GD.Print($"Found file: {filename}");
                filename = dir.GetNext();
            }
        }
        else
        {
            GD.Print("Check name of dir obstacle");
        }
        return pathes;
    }

    private void SpawnObject(float range)
    {
        if (player.GlobalPosition.X - spawnPoint > range)
        {
            Random rnd = new Random();
            int g = rnd.Next(0, obstaclesScenes.Count - 1);
            Node2D obstacle = (Node2D)obstaclesScenes[g].Instantiate();
            this.AddChild(obstacle);
            obstacle.GlobalPosition = new Vector2(GetViewport().GetVisibleRect().Size.X, this.GlobalPosition.Y);
            spawnPoint += range;
        }
    }
    private void SpawnObjectAtEndOfScreen()
    {
        Node2D obstacle = (Node2D)obstaclesScenes[2].Instantiate();
        this.AddChild(obstacle);
        obstacle.GlobalPosition = new Vector2(GetViewport().GetVisibleRect().Size.X, this.GlobalPosition.Y);
    }
}
