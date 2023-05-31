using Godot;
using System;

[Tool]
public partial class AudioStreamPlayerVFX : AudioStreamPlayerCustom
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.slider = globalSettings.VFXslider;
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
