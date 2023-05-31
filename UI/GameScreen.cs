using Godot;
using System;

public partial class GameScreen : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.
    [Export] protected Button restartButton, quitButton, optionsButton;
    protected SignalBus signalBus;
    protected Player player;
    protected Node root { get; set; }
    OptionsScreen options;
    GlobalSettings globalSettings;
    public override void _Ready()
    {
        globalSettings = GetNode<GlobalSettings>("/root/GlobalSettings");
        options = globalSettings.MyOptionsInstance;
        root = GetTree().Root;

        // signalBus.SaveScore += ChangeScore;
        // signalBus.PlayerEnteredScene += ParsePlayer;

    }
    public virtual void ConnectToNodes()
    {
        optionsButton.Connect("pressed", new Callable(this, "OpenOptions"));
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
        root = GetTree().Root;

        GetTree().Paused = false;
        root.RemoveChild(root.GetNode("TestLevel"));
        LoadGame();
    }
    public virtual void LoadGame()
    {
        PackedScene packedLevel = (PackedScene)ResourceLoader.Load("res://Levels/test_level.tscn");
        Node2D restartlevel = (Node2D)packedLevel.Instantiate();
        root.AddChild(restartlevel); //DID ALL OF THIS BECAUSE I NEED TO SAVE MEMORY FROM TEXTURES;
        signalBus.EmitSignal(SignalBus.SignalName.Restart);
    }
    public virtual void OpenOptions()
    {
        this.Visible = false;
        options.Visible = true;
        options.MyParent = this;
    }
}
