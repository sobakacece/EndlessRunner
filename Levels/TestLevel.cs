using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class TestLevel : Node2D
{
    Player player;
    TileMap tileMap;
    List<PackedScene> obstaclesScenes;
    SignalBus signalBus;
    Camera2D camera;
    float spawnPoint;
    [Export] float treeRangeSpawn;
    Random rnd = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = this.GetNode<Player>("Player");
        tileMap = this.GetNode<TileMap>("TileMap");
        spawnPoint = player.GlobalPosition.X;
        camera = this.GetNode<Camera2D>("Camera2D");

        obstaclesScenes = GetAllSpawnable("res://Obstacles/");

        // InstantiateObstacles();
        // UIManager = GetNode<UIManager>("/root/UIManager");
        // UIManager.LoadScore(player, camera);
        // GD.Print($"Player position: {player.GlobalPosition.ToString()}");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        tileMap.GlobalPosition = new Vector2(player.GlobalPosition.X, tileMap.GlobalPosition.Y);
        SpawnObjectAtEndOfScreen(treeRangeSpawn);

    }

    public List<PackedScene> GetAllSpawnable(string path)
    {
        List<PackedScene> pathes = new List<PackedScene>();
        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string filename = dir.GetNext();
            while (filename != "" && filename.Contains("tscn"))
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

    private void SpawnObjectAtEndOfScreen(float range)
    {

        if (player.GlobalPosition.X > spawnPoint) //spawn poin is a range where player appears. So when player starts walking, It spawns a tree.
        {
            Area2D obstacle = (Area2D)obstaclesScenes[rnd.Next(0, obstaclesScenes.Count)].Instantiate(); //spawn random object from list
            this.AddChild(obstacle);
            GD.Print($"Instance of {obstacle.Name} is created");
            float placeToSpawn = player.GlobalPosition.X + GetViewport().GetVisibleRect().Size.X; // distance to the screen edge, so trees can spawn behind the screen
            // 5 is camera zoom, 2 is half of the screen, so I guess 5*2? I'm not sure;
            // GD.Print($"Tree spawned at coords: {(placeToSpawn).ToString()}");
            obstacle.GlobalPosition = new Vector2(placeToSpawn, tileMap.GlobalPosition.Y - 40); //-20 is a half of object's sprite heigth
            spawnPoint += range; //there spawnpoint updates and player must go half of the screen to spawn another tree. The faster player goes, the faster tree spawns
        }

    }

}
