#if TOOLS
using Godot;
using System;

[Tool]
public partial class MusicSettings : EditorPlugin
{
	public override void _EnterTree()
	{
		var texture = GD.Load<Texture2D>("res://addons/MusicSettings/AudioStreamPlayer.svg");
		var MusicSript = GD.Load<Script>("res://addons/MusicSettings/AudioStreamPlayerMusic.cs");
		var VFXScript = GD.Load<Script>("res://addons/MusicSettings/AudioStreamPlayerVFX.cs");
		AddCustomType("AudioStreamPlayerMusic", "AudioStreamPlayer", MusicSript, texture);
		AddCustomType("AudioStreamPlayerVFX", "AudioStreamPlayer", VFXScript, texture);
	}

	public override void _ExitTree()
	{
		RemoveCustomType("AudioStreamPlayerVFX");
		RemoveCustomType("AudioStreamPlayerMusic");
		// Clean-up of the plugin goes here.
	}
}
#endif
