using Godot;
using System;

public partial class GlobalSettings : Node
{
    int busEffect, busMusic, busMaster;
    public Slider VFXslider, MusicSlider, GlobalSlider;
    public OptionsScreen MyOptionsInstance { get; set; }
    public CreditsScreen MyCreditsScreen { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        busEffect = AudioServer.GetBusIndex("Effect");
        busMusic = AudioServer.GetBusIndex("Music");
        busMaster = AudioServer.GetBusIndex("Master");

        PackedScene optionsScreen = (PackedScene)ResourceLoader.Load("res://UI/OptionsScreen.tscn");
        MyOptionsInstance = (OptionsScreen)optionsScreen.Instantiate();
        this.AddChild(MyOptionsInstance);
        MyOptionsInstance.Visible = false;

        PackedScene creditsScreen = (PackedScene)ResourceLoader.Load("res://UI/CreditsScreen.tscn");
        MyCreditsScreen = (CreditsScreen)creditsScreen.Instantiate();
        this.AddChild(MyCreditsScreen);
        MyCreditsScreen.Visible = false;

        // VFXslider.Connect("drag_ended", new Callable(this, "VFX"));
        GlobalSlider.Connect("drag_ended", new Callable(this, "Master"));
        // MusicSlider.Connect("drag_ended", new Callable(this, "Music"));

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    // public void VFX(bool value)
    // {
    //     ChangeVolume(busEffect, VFXslider);
    // }
    // public void Music(bool value)
    // {
    //     ChangeVolume(busMusic, MusicSlider);
    // }
    public void Master(bool value)
    {
        ChangeVolume(busMaster, GlobalSlider);

    }
    public void ChangeVolume(int busIndex, Slider slider)
    {
        if ((int)slider.Value == (int)slider.MinValue)
        {
            AudioServer.SetBusMute(busIndex, true);
        }
        else
        {
            AudioServer.SetBusMute(busIndex, false);
            AudioServer.SetBusVolumeDb(busIndex, (float)slider.Value);
        }
    }
}
