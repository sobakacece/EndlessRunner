using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
//TODO CREATE CLASS OF SPAWNER. ADD PROPERTIES RANGE, HEIGTH AND METHOD TO MAKE VISIBLE ON OF SPRITES. THEN PROCESS THERE WILL SPAWN ALL SPAWNERS.
public partial class TestLevel : Node2D
{
    Player player;
    TileMap tileMap;
    List<PackedScene> obstaclesScenes, cloudScenes;
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


        obstaclesScenes = GetAllSpawnable("res://Decorations/Obstacles/");
        cloudScenes = GetAllSpawnable("res://Decorations/Clouds/");
        // InstantiateObstacles();
        // UIManager = GetNode<UIManager>("/root/UIManager");
        // UIManager.LoadScore(player, camera);
        // GD.Print($"Player position: {player.GlobalPosition.ToString()}");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        tileMap.GlobalPosition = new Vector2(player.GlobalPosition.X, tileMap.GlobalPosition.Y);

        if (player.GlobalPosition.X > spawnPoint) //spawn poin is a range where player appears. So when player starts walking, It spawns a tree.
        {
            SpawnObjectAtEndOfScreen(treeRangeSpawn, obstaclesScenes, -60);
            SpawnObjectAtEndOfScreen(-40, cloudScenes, rnd.Next(-200, -100));
            spawnPoint += treeRangeSpawn; //there spawnpoint updates and player must go half of the screen to spawn another tree. The faster player goes, the faster tree spawns
        }

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

    private void SpawnObjectAtEndOfScreen(float range, List<PackedScene> sceneToSpawn, float heigth)
    {

        Node2D obstacle = (Node2D)sceneToSpawn[rnd.Next(0, sceneToSpawn.Count)].Instantiate(); //spawn random object from list
        this.AddChild(obstacle);
        GD.Print($"Instance of {obstacle.Name} is created");
        float placeToSpawn = player.GlobalPosition.X + GetViewport().GetVisibleRect().Size.X; // distance to the screen edge, so trees can spawn behind the screen
                                                                                              // 5 is camera zoom, 2 is half of the screen, so I guess 5*2? I'm not sure;
        obstacle.GlobalPosition = new Vector2(placeToSpawn, tileMap.GlobalPosition.Y + heigth); //-20 is a half of object's sprite heigth


    }

}
