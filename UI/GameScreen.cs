using Godot;
using System;

public partial class GameScreen : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.
    [Export] protected Button restartButton, quitButton, optionsButton, recordsButton;
    protected SignalBus signalBus;
    protected Player MyPlayer;
    protected Node root { get; set; }
    OptionsScreen options;
    RecordsScreen records;
    public GlobalSettings MyGlobalSettings { get => GetNode<GlobalSettings>("/root/GlobalSettings"); }
    public override void _Ready()
    {
        options = MyGlobalSettings.MyOptions;
        records = MyGlobalSettings.MyRecords;
        root = GetTree().Root;

        // signalBus.SaveScore += ChangeScore;
        // signalBus.PlayerEnteredScene += ParsePlayer;

    }
    public virtual void ConnectToNodes()
    {
        optionsButton.Connect("pressed", new Callable(this, "OpenOptions"));
        restartButton.Connect("pressed", new Callable(this, "Restart"));
        quitButton.Connect("pressed", new Callable(this, "Quit"));
        recordsButton.Connect("pressed", new Callable(this, "Records"));

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
        ChangeScreen(options);
        options.ReturnScreen = this;
    }
    public virtual void Records()
    {
        ChangeScreen(records);
        records.ReturnScreen = this;
    }

    public void ChangeScreen(GameScreen newScreen)
    {
        Visible = !Visible;
        newScreen.Visible = !Visible;
    }
}
