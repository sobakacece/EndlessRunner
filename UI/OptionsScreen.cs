using Godot;
using System;

public partial class OptionsScreen : GameScreen
{
    Slider VFXSlider { get => GetNode<Slider>("Overlay/VBoxContainer/OptionsContainer/VFXContainer/VFXSlider"); }
    Slider MusicSlider { get => GetNode<Slider>("Overlay/VBoxContainer/OptionsContainer/MusicContainer/MusicSlider"); }
    public Slider GlobalSlider { get => GetNode<Slider>("Overlay/VBoxContainer/OptionsContainer/GlobalContainer/GlobalSlider"); }
    BaseButton fullScreenButton { get => GetNode<BaseButton>("Overlay/VBoxContainer/OptionsContainer/FullScreenContainer/FullScreenCheckBox"); }
    Button creditsButton { get => GetNode<Button>("Overlay/VBoxContainer/VBoxContainer/CreditsButton"); }

    public GameScreen ReturnScreen { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        MyGlobalSettings.VFXslider = this.VFXSlider;
        MyGlobalSettings.MusicSlider = this.MusicSlider;
        MyGlobalSettings.GlobalSlider = this.GlobalSlider;
        ConnectToNodes();


    }

    public void OpenCredits()
    {
       ChangeScreen(MyGlobalSettings.MyCredits);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void ConnectToNodes()
    {
        quitButton.Connect("pressed", new Callable(this, "Quit"));
        fullScreenButton.Connect("toggled", new Callable(this, "ChangeScreenDimension"));
        creditsButton.Connect("pressed", new Callable(this, "OpenCredits"));
    }
    public override void Quit()
    {
        ChangeScreen(ReturnScreen);
    }
    public void ChangeScreenDimension(bool button_pressed)
    {
        DisplayServer.WindowSetMode((DisplayServer.WindowGetMode() != DisplayServer.WindowMode.Fullscreen) ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);

    }
}
