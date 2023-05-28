using Godot;
using System;

public partial class GameScreen : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.
    [Export] protected Button restartButton, quitButton;
    protected SignalBus signalBus;
    protected Player player;

    public override void _Ready()
    {

        // signalBus.SaveScore += ChangeScore;
        // signalBus.PlayerEnteredScene += ParsePlayer;

    }
    public virtual void ConnectToNodes()
    {
        restartButton.Connect("pressed", new Callable(this, "Restart"));
        quitButton.Connect("pressed", new Callable(this, "Quit"));
        signalBus = GetNode<SignalBus>("/root/SignalBus");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public virtual void Quit()
    {
        GetTree().Quit();
    }
    public virtual void Restart()
    {
        GetTree().Paused = false;
        Node root = GetNode("/root");
        root.RemoveChild(root.GetNode("TestLevel"));
        PackedScene packedLevel = (PackedScene)ResourceLoader.Load("res://Levels/test_level.tscn");
        Node2D restartlevel = (Node2D)packedLevel.Instantiate();
        root.AddChild(restartlevel); //DID ALL OF THIS BECAUSE I NEED TO SAVE MEMORY FROM TEXTURES;

        signalBus.EmitSignal(SignalBus.SignalName.Restart);
    }
}
