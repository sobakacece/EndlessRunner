using Godot;
using System;

public partial class MainMenuScreen : GameScreen
{
    [Export] AudioStreamPlayer mainMenuAudio;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetTree().Paused = true;
        ConnectToNodes();
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void ConnectToNodes()
    {
        base.ConnectToNodes();
        restartButton.Disconnect("pressed", new Callable(this, "Restart"));
        restartButton.Connect("pressed", new Callable(this, "LoadGame"));

    }
    public override void LoadGame()
    {
        mainMenuAudio.Stop();
        this.Visible = false;
        base.LoadGame();
        GetTree().Paused = false;
        MyGlobalSettings.GlobalMusic.Play();

    }


}
