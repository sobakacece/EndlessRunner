using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
//TODO  horizontal rockets, fix bug with clouds moving backwards, rethink spawner system, pause screen, UI animations?, slow after a hit, decrease possible angle of attack, best score, level change, change death animation
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

        if (player.GlobalPosition.X - spawner.SpawnPoint >= spawner.MySpawnRange) //If player passed spawnRange
        {
            this.AddChild(spawner);
            float placeToSpawn = player.GlobalPosition.X + GetViewport().GetVisibleRect().Size.X / 2 + spawner.MySpawnRange; //player position + outside of screen + range
            spawner.GlobalPosition = new Vector2(placeToSpawn, tileMap.GlobalPosition.Y + spawner.MyHeight); //floor + custom height
            spawnerList[spawnerCounter] = InstantiateSpawner(spawner.MyPath); //instanciate new spawner with new parameters (spawnPoint, Range, e.t.c) and replace them
        }


    }
    private Spawner InstantiateSpawner(string path)
    {
        PackedScene scene = (PackedScene)ResourceLoader.Load(path);
        Spawner spawner = (Spawner)scene.Instantiate();
        spawner.SpawnPoint = player.GlobalPosition.X + spawner.MySpawnRange; //stores old position of player
        return spawner;
    }
}
