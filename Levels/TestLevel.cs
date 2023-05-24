using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
//TODO CREATE CLASS OF SPAWNER. ADD PROPERTIES RANGE, HEIGTH AND METHOD TO MAKE VISIBLE ON OF SPRITES. THEN PROCESS THERE WILL SPAWN ALL SPAWNERS.
public partial class TestLevel : Node2D
{
    Player player;
    TileMap tileMap;
    // List<PackedScene> obstaclesScenes, cloudScenes;
    List<Spawner> spawnerList = new List<Spawner>();
    List<PackedScene> scenes = new List<PackedScene>();
    SignalBus signalBus;
    Camera2D camera;
    float spawnPoint;
    int i;
    Spawner spawnerTree;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = this.GetNode<Player>("Player");
        tileMap = this.GetNode<TileMap>("TileMap");
        spawnPoint = player.GlobalPosition.X;
        camera = this.GetNode<Camera2D>("Camera2D");

        spawnerTree = InstantiateSpawner("res://Spawners/tree_spawner.tscn");


        spawnerList = GetAllSpawnable("res://Spawners/");
        // PackedScene scene = (PackedScene)ResourceLoader.Load("res://Spawners/tree_spawner.tscn");

        // cloudScenes = GetAllSpawnable("res://Decorations/Clouds/");

        // treeSpawner = InstantiateSpawner("res://Decorations/tree_spawner.tscn");
        // cloudSpawner = InstantiateSpawner("res://Decorations/tree_spawner.tscn");

        // InstantiateObstacles();
        // UIManager = GetNode<UIManager>("/root/UIManager");
        // UIManager.LoadScore(player, camera);
        // GD.Print($"Player position: {player.GlobalPosition.ToString()}");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        tileMap.GlobalPosition = new Vector2(player.GlobalPosition.X, tileMap.GlobalPosition.Y);

        // for (i = 0; i < spawnerList.Count; i++)
        // {
        //     Spawn(spawnerList[i]);
        //     GD.Print($"Player position {player.GlobalPosition.X} SpawnPoint of {spawnerList[i].Name} is {spawnerList[i].SpawnPoint}");


        // }
        Spawn(spawnerTree);
        // Spawn(treeSpawner);
        // Spawn(cloudSpawner);
        // GD.Print($"Player position {player.GlobalPosition.X} SpawnPoint of {spawnerList[i].Name} is {spawnerList[i].SpawnPoint}");


    }

    public List<Spawner> GetAllSpawnable(string path)
    {
        GD.Print("spawners");
        List<Spawner> pathes = new List<Spawner>();
        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string filename = dir.GetNext();
            while (filename != "" && filename.Contains("tscn"))
            {
                // PackedScene scene = (PackedScene)ResourceLoader.Load(path + filename);
                // scenes.Add(scene);
                pathes.Add(InstantiateSpawner(path + filename));
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

    // private void SpawnObjectAtEndOfScreen(float range, List<PackedScene> sceneToSpawn, float heigth)
    // {

    //     Node2D obstacle = (Node2D)sceneToSpawn[rnd.Next(0, sceneToSpawn.Count)].Instantiate(); //spawn random object from list
    //     this.AddChild(obstacle);
    //     GD.Print($"Instance of {obstacle.Name} is created");
    //     float placeToSpawn = player.GlobalPosition.X + GetViewport().GetVisibleRect().Size.X; // distance to the screen edge, so trees can spawn behind the screen
    //                                                                                           // 5 is camera zoom, 2 is half of the screen, so I guess 5*2? I'm not sure;
    //     obstacle.GlobalPosition = new Vector2(placeToSpawn, tileMap.GlobalPosition.Y + heigth); //-20 is a half of object's sprite heigth


    // }
    private void Spawn(Spawner spawner)
    {
        // GD.Print($"Player position: {player.GlobalPosition.X}, Spawn Point: {obstacle.SpawnPoint}");
        // Spawner obstacle = (Spawner)treeSpawner.Instantiate();

        if (player.GlobalPosition.X - spawner.SpawnPoint >= spawner.MySpawnRange) //spawn poin is a range where player appears. So when player starts walking, It spawns a tree.
        {
            this.AddChild(spawner);
            float placeToSpawn = player.GlobalPosition.X + GetViewport().GetVisibleRect().Size.X; // distance to the screen edge, so trees can spawn behind the screen
                                                                                                  // 5 is camera zoom, 2 is half of the screen, so I guess 5*2? I'm not sure;
            spawner.GlobalPosition = new Vector2(placeToSpawn, tileMap.GlobalPosition.Y + spawner.MyHeight); //-20 is a half of object's sprite heigth
            spawnerTree = InstantiateSpawner(spawner.MyPath);
            // spawnerTree.SpawnPoint += spawner.MySpawnRange; //this spawner is the new instance of original one. It's SpawnPoint = Player's position;
                                                               // obstacle.SpawnPoint += obstacle.MySpawnRange; //there spawnpoint updates and player must go half of the screen to spawn another tree. The faster player goes, the faster tree spawns
        }


        //spawn random object from list

        // GD.Print($"Instance of {obstacle.Name} is created");
    }
    private Spawner InstantiateSpawner(string path)
    {
        PackedScene scene = (PackedScene)ResourceLoader.Load(path);
        Spawner spawner = (Spawner)scene.Instantiate();
        spawner.SpawnPoint = player.GlobalPosition.X + spawner.MySpawnRange;
        // spawner.PickRandom();
        return spawner;
    }

}
