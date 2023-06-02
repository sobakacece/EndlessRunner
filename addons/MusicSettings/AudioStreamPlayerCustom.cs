using Godot;
using System;

[Tool]
public partial class AudioStreamPlayerCustom : AudioStreamPlayer
{
    protected GlobalSettings globalSettings { get => GetNode<GlobalSettings>("/root/GlobalSettings"); }
    protected Slider slider;
    int busIndex;
    public override void _Ready()
    {
        busIndex = AudioServer.GetBusIndex(this.Bus);
        // GD.Print("Ready");
        this.VolumeDb = (float)slider.Value;
        this.StreamPaused = false;
        if ((int)slider.Value == (int)slider.MinValue)
        {
            AudioServer.SetBusMute(busIndex, true);


        }
        slider.Connect("drag_ended", new Callable(this, "VolumeChange"));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {


    }
    public void VolumeChange(bool value)
    {

        if ((int)slider.Value == (int)slider.MinValue)
        {
            AudioServer.SetBusMute(busIndex, true);
        }
        else
        {
            AudioServer.SetBusMute(busIndex, false);
            this.VolumeDb = (float)slider.Value;
        }

    }

}
