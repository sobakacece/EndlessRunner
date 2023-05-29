using Godot;
using System;

public partial class Parallax : ParallaxBackground
{
    Player player;
    Spawner cloudSpawner;
    ParallaxLayer cloudLayer;
    public override void _Ready()
    {
        player = (Player)GetNode("/root/TestLevel/Player");

        cloudLayer = GetNode<ParallaxLayer>("Clouds");
        cloudSpawner = InstantiateSpawner();
        // cloudLayer.MotionOffset = new Vector2(-player.speed, 0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Spawn(cloudSpawner);
    }
    public void Spawn(Spawner spawner)
    {
        if (this.player.GlobalPosition.X - cloudSpawner.SpawnPoint >= spawner.MySpawnRange) //If player passed spawnRange
        {
            cloudLayer.AddChild(spawner);
            float placeToSpawn = GetViewport().GetVisibleRect().Size.X / 2 + spawner.MySpawnRange + this.player.GlobalPosition.X; //player position + outside of screen + range
            spawner.Position = new Vector2(placeToSpawn, spawner.MyHeight); //floor + custom height
            GD.Print($"CloudSpawned at {spawner.MyHeight}");

            cloudSpawner = InstantiateSpawner();//instanciate new spawner with new parameters (spawnPoint, Range, e.t.c) and replace them
        }
    }
    public Spawner InstantiateSpawner()
    {
        PackedScene cloudSpawnerScene = (PackedScene)ResourceLoader.Load("res://Levels/Parallax/cloud_spawner.tscn");
        Spawner spawner = (Spawner)cloudSpawnerScene.Instantiate();
        spawner.SpawnPoint = this.player.GlobalPosition.X + spawner.MySpawnRange; //stores old position of player
        return spawner;
    }
}
