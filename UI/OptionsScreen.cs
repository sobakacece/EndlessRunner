using Godot;
using System;

public partial class OptionsScreen : GameScreen
{
    Slider VFXSlider { get => GetNode<Slider>("Overlay/VBoxContainer/OptionsContainer/VFXContainer/VFXSlider"); }
    Slider MusicSlider { get => GetNode<Slider>("Overlay/VBoxContainer/OptionsContainer/MusicContainer/MusicSlider"); }
    Slider GlobalSlider { get => GetNode<Slider>("Overlay/VBoxContainer/OptionsContainer/GlobalContainer/GlobalSlider"); }
    BaseButton fullScreenButton { get => GetNode<BaseButton>("Overlay/VBoxContainer/OptionsContainer/FullScreenContainer/FullScreenCheckBox"); }
    GlobalSettings global;
    public GameScreen MyParent { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<GlobalSettings>("/root/GlobalSettings");

        global.VFXslider = this.VFXSlider;
        global.MusicSlider = this.MusicSlider;
        global.GlobalSlider = this.GlobalSlider;
        ConnectToNodes();

        fullScreenButton.Connect("toggled", new Callable(this, "ChangeScreenDimension"));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void ConnectToNodes()
    {
        quitButton.Connect("pressed", new Callable(this, "Quit"));
    }
    public override void Quit()
    {
        MyParent.Visible = true;
        this.Visible = false;
    }
    public void ChangeScreenDimension(bool button_pressed)
    {
        DisplayServer.WindowSetMode((DisplayServer.WindowGetMode() != DisplayServer.WindowMode.Fullscreen) ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);

    }
}
