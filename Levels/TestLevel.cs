using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
//TODO Jump dependencie from time SPACE was held, add more spawners, fix bug with clouds moving backwards
public partial class TestLevel : Node2D
{
    private Player player;
    private TileMap tileMap;
    private List<Spawner> spawnerList = new List<Spawner>();
    private SignalBus signalBus;
    private Camera2D camera;
    private int spawnerCounter;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = this.GetNode<Player>("Player");
        tileMap = this.GetNode<TileMap>("TileMap");
        camera = this.GetNode<Camera2D>("Camera2D");

        spawnerList = GetAllSpawnable("res://Spawners/");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        tileMap.GlobalPosition = new Vector2(player.GlobalPosition.X, tileMap.GlobalPosition.Y);

        for (spawnerCounter = 0; spawnerCounter < spawnerList.Count; spawnerCounter++)
        {
            Spawn(spawnerList[spawnerCounter]);
        }
    }

    public List<Spawner> GetAllSpawnable(string path)
    {
        List<Spawner> pathes = new List<Spawner>();
        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string filename = dir.GetNext();
            GD.Print($"Found file: {filename}");
            while (filename != "")
            {
                if (filename.Contains("tscn"))
                {
                pathes.Add(InstantiateSpawner(path + filename));
                // GD.Print($"Found file: {filename}");
                }
                 
                filename = dir.GetNext();
            }
        }
        else
        {
            GD.Print("Check name of dir spawners");
        }
        return pathes;
    }



    // }
    private void Spawn(Spawner spawner)
    {

        if (player.GlobalPosition.X - spawner.SpawnPoint >= spawner.MySpawnRange) //spawn poin is a range where player appears. So when player starts walking, It spawns a tree.
        {
            this.AddChild(spawner);
            float placeToSpawn = player.GlobalPosition.X + GetViewport().GetVisibleRect().Size.X / 2 + 10; // distance to the screen edge, so trees can spawn behind the screen
                                                                                                  // player is aprox at the center of the screen, so we need only half of it, so we divide on 2.
                                                                                                  // 10 is an offset to spawn behind the border and not on it
            spawner.GlobalPosition = new Vector2(placeToSpawn, tileMap.GlobalPosition.Y + spawner.MyHeight); //-20 is a half of object's sprite heigth
            spawnerList[spawnerCounter] = InstantiateSpawner(spawner.MyPath);
        }


    }
    private Spawner InstantiateSpawner(string path)
    {
        PackedScene scene = (PackedScene)ResourceLoader.Load(path);
        Spawner spawner = (Spawner)scene.Instantiate();
        spawner.SpawnPoint = player.GlobalPosition.X + spawner.MySpawnRange;
        return spawner;
    }
}
